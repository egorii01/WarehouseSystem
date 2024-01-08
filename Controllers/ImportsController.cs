using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers.ImportsController
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

        [HttpGet]
        public IActionResult Create(string? invoice)
        {   
            
            return View();
        } 

    }
}