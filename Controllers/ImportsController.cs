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
        public IActionResult Create(int? invoiceId)
        {   
            _logger.LogInformation("Get Create in Imports");
            
           //создаем объект импорта для модели
           Import creatingImport = new Import();
           creatingImport.InvoiceID = invoiceId;

            return View();
        } 

    }
}