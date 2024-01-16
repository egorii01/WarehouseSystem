using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly StockContext _context;
        private readonly ILogger<Invoice> _logger;

        public InvoicesController(StockContext context, ILogger<Invoice> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {

            var invoices = _context.Invoices
                .Where(i => i.Actual != false)
                .Include(i => i.Responsible);

            foreach (Invoice i in invoices)
            {
                _context.Employees.Where(e => e.Id == i.ResponsibleID).Load();

            }

            return View(await invoices.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Responsible)
                .FirstOrDefaultAsync(m => m.Id == id);

            
            if (invoice == null)
            {
                return NotFound();
            }

            var imports = await _context.Imports
                .Where(i => i.InvoiceID == id)
                .Include(i => i.Product)
                .ToListAsync();

            invoice.Imports = imports;

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {

            employeesDropdownList();
            return View(new Invoice());
        }

        [HttpPost]
        public IActionResult GetCreateImportForm([FromBody]List<Import> imports)
        {
            if (imports != null)
            {
                _logger.LogInformation("imports is not null");
                foreach (Import import in imports)
                {

                    _logger.LogInformation($"import.Product: {import.ProductID} import.Quantity: {import.Quantity}");

                }

                string testJson = JsonConvert.SerializeObject(imports);
                _logger.LogInformation($"testJson: {testJson}");
                TempData["imports"] = JsonConvert.SerializeObject(imports);
            }
            else 
            {
                _logger.LogInformation("imports is null");
            }

            

            ViewBag.ProductsList = getProductList();
            return PartialView("../Imports/_CreateImport", new Import());
        }

        private SelectList getProductList(object selectedPosition = null)
        {
            var productQuery = from p in _context.Products
            where p.Actual != false
            orderby p.Name
            select p;

            return new SelectList(productQuery.AsNoTracking(), "Id", "Name", selectedPosition);
        } 

        [HttpPost]
        public ActionResult UpdateImports([FromBody]Import import) 
        {
            if (import == null)
            {
                _logger.LogInformation("import is null");
            }
            else
            {
                _logger.LogInformation($"import.Product: {import.ProductID} import.Quantity: {import.Quantity}");
            }

            string importsJson = TempData["imports"].ToString();
            List<Import>? imports = JsonConvert.DeserializeObject<List<Import>>(importsJson);
            
            
            if (imports != null)
            {
                _logger.LogInformation("imports is not null");
                imports.Add(import);
                _logger.LogInformation($"import.ProductID: {import.ProductID}");

                foreach (Import importObject in imports)
                {
                    importObject.Product = _context.Products.Where(p => p.Id == importObject.ProductID).FirstOrDefault();
                    
                    if (importObject.Product == null)
                    {
                        _logger.LogInformation($"importObject.Product is null");
                    }
                    else
                    {
                        _logger.LogInformation($"importObject.Product is not null");
                    }
                }

            }

            _logger.LogInformation($"imports.Count: {imports.Count}");

            TempData["imports"] = JsonConvert.SerializeObject(imports);

            return PartialView("../Imports/_ImportsTable", imports);
            
            //return Json(new { success = true });

        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ResponsibleID")] Invoice invoice)
        {

            //десериализуем импорты, указанные в таблице на форме
            string importsJson = TempData["imports"].ToString();
            _logger.LogInformation($"importsJson: {importsJson}");
            List<Import>? imports = JsonConvert.DeserializeObject<List<Import>>(importsJson);

            //проставляем null в Product, чтобы не пытаться создавать существующие записи в бд
            foreach(Import import in imports)
            {
                import.Product = null;
            }

            //записываем импорты к фактуре
            invoice.Imports = imports;
            invoice.Time = DateTime.Now;

            //if (ModelState.IsValid)
            //{
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            // }
            // return View(invoice);
        }

        private void employeesDropdownList(object selectedPosition = null)
        {
            var employeesQuery = from e in _context.Employees
            orderby e.Id
            where e.Actual != false
            select e;

            ViewBag.ResponsibleID = new SelectList(employeesQuery.AsNoTracking(), "Id", "FullName", selectedPosition);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Responsible)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            var imports = await _context.Imports
                .Where(i => i.InvoiceID == id)
                .Include(i => i.Product)
                .ToListAsync();

            invoice.Imports = imports;

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                invoice.Actual = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
