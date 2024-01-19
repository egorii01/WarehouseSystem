using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
                .Include(s => s.Cashier);

            foreach (Check sale in sales)
            {
                _context.Employees.Where(e => e.Id == sale.CashierID).Load();
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
            where e.Actual != false
            select e;

            ViewBag.CashierID = new SelectList(employeesQuery.AsNoTracking(), "Id", "FullName", selectedPosition);
        }

    }

}