using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

            var invoices = _context.Invoices.Include(i => i.Responsible);

            foreach (Invoice i in invoices)
            {
                _context.Employees.Where(e => e.Id == i.ResponsibleID).Load();

            }

            return View(await _context.Invoices.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {

            employeesDropdownList();
            return View(new Invoice());
        }

        public IActionResult GetCreateImportForm()
        {
            ViewBag.ProductsList = getProductList();
            return PartialView("../Imports/_CreateImport", new Import());
        }

        private SelectList getProductList(object selectedPosition = null)
        {
            var productQuery = from p in _context.Products
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
            return PartialView("../Imports/_ImportsTable", new List<Import>());
            //return Json(new { success = true });

        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        private void employeesDropdownList(object selectedPosition = null)
        {
            var employeesQuery = from e in _context.Employees
            orderby e.Id
            where e.Actual != false
            select e;

            ViewBag.ResponsibleID = new SelectList(employeesQuery.AsNoTracking(), "Id", "FullName", selectedPosition);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Time")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

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
                _context.Invoices.Remove(invoice);
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
