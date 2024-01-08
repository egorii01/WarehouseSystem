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

            /*var invoice = await _context.Invoices
                .FirstOrDefaultAsync(m => m.Id == id);*/

            //var invoice = _context.Invoices;

            Invoice? invoice = await _context.Invoices
                .Where(i => i.Id == id)
                .Include(i => i.Imports)
                .Include(i => i.Responsible)
                .FirstOrDefaultAsync();


            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                _logger.LogInformation("invoice is not null");
                ICollection<Import> imports = new List<Import>();
                foreach (Import import in invoice.Imports)
                {
                    Import? import1 = await _context.Imports
                        .Where(i => i.Id == import.Id)
                        .Include(i => i.Product)
                        .Include(nameof(Invoice))
                        .FirstOrDefaultAsync();

                    if (import1 != null)
                    {
                        imports.Add(import1);
                    }
                }

                if (imports.Count() > 0) {
                    invoice.Imports = imports;
                }

                foreach (Import import in invoice.Imports)
                {
                    _logger.LogInformation($"import.Product.Name: {import.Product.Name}, import.Quantity: {import.Quantity}");
                }
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            //создаем в представлении кода объекты поступления и списка импортов
            Invoice model = new Invoice();
            model.Imports = new List<Import>();
            
            employeesDropdownList();
            return View(model);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Time,ResponsibleID")] Invoice invoice)
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

        //не понятно, пригодится ли этот кусок кода...
        /*// GET: Invoices/Delete/5
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
        }*/

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
