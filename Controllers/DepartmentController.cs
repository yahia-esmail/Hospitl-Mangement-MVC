using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospitl_Mangement_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly HospitalDbContext _context;

        public DepartmentController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Doctor/Create edit it 
        public IActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_context.Department, "Id", "DepartmentName");
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentName,Location")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewAll));
            }
            return View();
        }
        public async Task<IActionResult> ViewAll()
        {
            return View(_context.Department.ToList());
        }
        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", department.Id);
            return View(department);
        }
        // POST: Department/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,Location")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var U_department = await _context.Department.FindAsync(id);
                    U_department.DepartmentName = department.DepartmentName;
                    U_department.Location = department.Location;
                    _context.Update(U_department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ViewAll));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", department.Id);
            return RedirectToAction(nameof(ViewAll));
        }

        // The missing DoctorExists method added here
        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Department.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewAll)); // Ensure this redirects to your list page
        }
    }
}
