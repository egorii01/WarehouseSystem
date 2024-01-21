using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers;

public class StockController : Controller
{
    private readonly ILogger<StockController> _logger;
    private readonly StockContext _context;

    public StockController(ILogger<StockController> logger, StockContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        List<Stock> stock = await _context.StockRecords
            .Include(s => s.Product).ToListAsync();
        return View(stock);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
