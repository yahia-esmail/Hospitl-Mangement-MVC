using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospitl_Mangement_MVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly HospitalDbContext _context;

        public StaffController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Staff/CreateNurse
        public IActionResult CreateNurse()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(x => x.Name != "Doctor" && x.Name != "Patient"), "Id", "Name");
            return View();
        }

        // POST: Staff/CreateNurse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNurse([Bind("First_Name,Last_Name")] Staff staff)
        {
            if (ModelState.IsValid )
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewAll));
            }
            //["RoleId"] = new SelectList(_context.Roles.Where(x => x.Name != "Doctor" && x.Name != "Patient"), "Id", "Name", staff.RoleId);
            return View(staff);
        }
        // POST: Staff/CreateNurse

        // Action to display all staff members
       public IActionResult ViewAll()
        {
            return View(_context.Staff.Include(x=>x.Role).ToList());
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", staff.DepartmentId);
            return View(staff);
        }
        // POST: Nurse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,First_Name,Last_Name")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var U_staff = await _context.Staff.FindAsync(id);
                    U_staff.First_Name = staff.First_Name;
                    U_staff.Last_Name = staff.Last_Name;
                    _context.Update(U_staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NurseExists(staff.Id))
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

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", staff.DepartmentId);
            return RedirectToAction(nameof(ViewAll));
        }

        // The missing DoctorExists method added here
        private bool NurseExists(string id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewAll)); // Ensure this redirects to your list page
        }


    }

}
