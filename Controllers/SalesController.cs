using Microsoft.AspNetCore.Mvc;
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

            
            return View();

        }

    }

}