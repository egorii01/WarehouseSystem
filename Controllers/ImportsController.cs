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
        public IActionResult Create()
        {   
            _logger.LogInformation("Get Create in Imports");

            Invoice? invoice = (Invoice?)TempData["invoice"];
            if (invoice == null)
            {
                _logger.LogInformation("Invoice is null!");
            }
            else 
            {
                _logger.LogInformation($"invoice.ResponsibleID: {invoice.ResponsibleID}");
            }
            

            return View();
        } 

    }
}