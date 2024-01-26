using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers
{

    public class SalesController : Controller
    {

        private readonly StockContext _context;
        private readonly ILogger<Check> _logger;

        public SalesController(StockContext context, ILogger<Check> logger)
        {

            _context = context;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {

            var sales = _context.Checks
                .Where(s => s.Actual != false)
                .Include(s => s.Cashier);

            foreach (Check sale in sales)
            {
                _context.Employees.Where(e => e.Id == sale.CashierID).Load();
                sale.CheckEntries = await _context.CheckEntries.Where(e => e.CheckId == sale.Id).ToListAsync();

                foreach (CheckEntry entry in sale.CheckEntries)
                {
                    entry.Product = _context.Products.Where(p => p.Id == entry.ProductID).FirstOrDefault();
                }
            }


            return View(await sales.ToListAsync());

        }

        // GET: Invoices/Create
        public IActionResult Create()
        {

            employeesDropdownList();
            return View(new Check());
        }

        private void employeesDropdownList(object selectedPosition = null)
        {
            var employeesQuery = from e in _context.Employees
            orderby e.Id
            where e.Actual != false && e.Position.Name == "Кассир" 
            select e;

            ViewBag.CashierID = new SelectList(employeesQuery.AsNoTracking(), "Id", "FullName", selectedPosition);
        }
    
        [HttpPost]
        public async Task<IActionResult> GetCreateEntryForm([FromBody]List<CheckEntry> entries)
        {

            if (entries != null)
            {
                _logger.LogInformation("entries is not null");
                foreach (CheckEntry entry in entries)
                {

                    _logger.LogInformation($"entry.Product: {entry.ProductID} entry.Quantity: {entry.Quantity}");

                }

                string testJson = JsonConvert.SerializeObject(entries);
                _logger.LogInformation($"testJson: {testJson}");
                TempData["entries"] = JsonConvert.SerializeObject(entries);
            }
            else 
            {
                _logger.LogInformation("entries is null");
            }

            

            ViewBag.ProductsList = getProductList();
            return PartialView("../CheckEntries/_CreateEntry", new CheckEntry());

        }

        private SelectList getProductList(object selectedPosition = null)
        {
            var productQuery = from p in _context.Products
            join s in _context.StockRecords on p.Id equals s.ProductId
            where p.Actual != false
            orderby p.Name
            select p;

            return new SelectList(productQuery.AsNoTracking(), "Id", "Name", selectedPosition);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductPrice([FromBody]CheckEntry entry)
        {

            if (entry == null)
            {
                _logger.LogWarning("entry is null!");

            }
            else
            {
                _logger.LogInformation("entry is not null");

                if (entry.ProductID != 0)
                {
                    Product? product = await _context.Products
                        .Where(p => p.Id == entry.ProductID)
                        .FirstOrDefaultAsync();

                    entry.Product = product;
                }

                return PartialView("../CheckEntries/_PriceInfo", entry);
            }

            return PartialView("../CheckEntries/_PriceInfo");
        }

        [HttpPost]
        public async Task<IActionResult> GetProductStockQuantity([FromBody] CheckEntry entry)
        {

            Stock? stockProductRecord = await _context.StockRecords
                .Where(s => s.ProductId == entry.ProductID)
                .FirstOrDefaultAsync();

            if (stockProductRecord != null) 
            {
                entry.MaxQuantity = stockProductRecord.Quantity;
            }
            else
            {
                entry.MaxQuantity = 0;
            }

            return Json(entry);
        } 

        [HttpPost]
        public async Task<IActionResult> UpdateEntries([FromBody]CheckEntry entry)
        {

            if (entry == null)
            {
                _logger.LogWarning("entry is null!");
            }
            else
            {
                _logger.LogInformation("entry is not null");
            }

            string entryJson = TempData["entries"].ToString();
            List<CheckEntry>? entries = JsonConvert.DeserializeObject<List<CheckEntry>>(entryJson);

            if (entries != null)
            {
                _logger.LogInformation("entries is not null");
                entries.Add(entry);

                foreach (CheckEntry entryObject in entries)
                {
                    entryObject.Product = _context.Products.Where(p => p.Id == entryObject.ProductID).FirstOrDefault();

                    if (entryObject.Product == null)
                    {
                        _logger.LogInformation("entryObject.Product is null");
                    }
                    else 
                    {
                        _logger.LogInformation("entryObject.Product is not null");
                    }
                }
            }

            TempData["entries"] = JsonConvert.SerializeObject(entries);

            return PartialView("../CheckEntries/_EntriesTable", entries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, CashierID")]Check check)
        {

            //десериализуем позиции чека
            string entriesJson = TempData["entries"].ToString();
            _logger.LogInformation($"entriesJson: {entriesJson}");
            List<CheckEntry>? entries = JsonConvert.DeserializeObject<List<CheckEntry>>(entriesJson);
            
            //проставляем null у товаров, чтобы не пытаться создать существующие записи в БД
            foreach (CheckEntry entry in entries)
            {

                Stock? stock = _context.StockRecords
                    .Where(s => s.ProductId == entry.ProductID)
                    .FirstOrDefault();

                if (stock == null)
                {
                    //TODO: нужно как-то обработать отсутствие товаров на складе
                }
                else
                {

                    stock.Quantity -= entry.Quantity;

                }


                entry.Product = null;
            }

            //записываем позиции чека к чеку
            check.CheckEntries = entries;
            check.Time = DateTime.Now;
            check.Actual = true;

            _context.Add(check);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Checks
                .Include(s => s.Cashier)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            var entries = await _context.CheckEntries
                .Where(e => e.CheckId == id)
                .Include(e => e.Product)
                .ToListAsync();

            sale.CheckEntries = entries;

            return View(sale);

        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Checks
                .Include(i => i.Cashier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            var imports = await _context.CheckEntries
                .Where(i => i.CheckId == id)
                .Include(i => i.Product)
                .ToListAsync();

            invoice.CheckEntries = imports;

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Checks.FindAsync(id);
            if (invoice != null)
            {
                invoice.Actual = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }

}