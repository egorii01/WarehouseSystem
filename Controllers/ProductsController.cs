using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Controllers
{

    public class ProductsController : Controller
    {

        private readonly ILogger<Product> _logger;
        private readonly StockContext _context;

        public ProductsController(StockContext context, ILogger<Product> logger)
        {

            _context = context;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Products.ToListAsync());

        }

        // GET: Employers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Price")] Product product)
        {

            if (ModelState.IsValid)
            {
                product.Actual = true;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }

    }

}