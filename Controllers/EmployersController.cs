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
    public class EmployersController : Controller
    {
        private readonly StockContext _context;
        private readonly ILogger<Employee> _logger;

        public EmployersController(StockContext context, ILogger<Employee> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Employers
        public async Task<IActionResult> Index()
        {
            var employee = _context.Employees.Where(e => e.Actual != false);
            var employees = employee.Include(e => e.Position);

            foreach (Employee e in employees)
            {
                _context.Positions.Where(p => p.Id == e.Position.Id).Load();
            }
            return View(await employees.ToListAsync());
        }

        // GET: Employers/Create
        public IActionResult Create()
        {
            positionsDropdownList();
            return View();
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,MiddleName, PositionID")] Employee employee)
        {

            _logger.LogInformation("Post Create called");
            _logger.LogInformation("ModelState.IsValid: {0}", ModelState.IsValid.ToString());

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            positionsDropdownList(employee.PositionID);
            return View(employee);
        }

        // GET: Employers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            positionsDropdownList(employee.PositionID);
            return View(employee);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeToUpdate = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, 
                "", 
                e => e.FirstName, e => e.MiddleName, e => e.LastName, e => e.PositionID)
            )
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            positionsDropdownList(employeeToUpdate.PositionID);
            return View(employeeToUpdate);
        }

        private void positionsDropdownList(object selectedPosition = null)
        {
            var positionsQuery = from p in _context.Positions
            orderby p.Id
            select p;

            ViewBag.PositionID = new SelectList(positionsQuery.AsNoTracking(), "Id", "Name", selectedPosition);
        }

        // GET: Employers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
