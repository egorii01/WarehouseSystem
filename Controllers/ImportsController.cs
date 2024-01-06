using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers
{
    public class ImportsController : Controller
    {

        private readonly StockContext _context;
        private readonly ILogger<Import> _logger;

        public ImportsController(StockContext context, ILogger<Import> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult CreateImport()
        {
            return RedirectToAction("Invoices/Create");
        }

    }
}