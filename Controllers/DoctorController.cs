using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospitl_Mangement_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName");
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Speciatly,DepartmentId,First_Name,Last_Name")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Viewall));
            }
            else
            {
                // Collect missing or invalid data fields
                var errorMessages = ModelState.Where(ms => ms.Value.Errors.Count > 0)
                                              .Select(ms => ms.Key) // Get the field names
                                              .ToList();

                // Create a message for the missing or invalid fields
                string missingDataMessage = "The following fields have errors or missing data: " + string.Join(", ", errorMessages);

                // Add the message to the ModelState, so it can be displayed
                ModelState.AddModelError(string.Empty, missingDataMessage);

                // Re-populate the Department dropdown in case of validation errors
                ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", doctor.DepartmentId);

                // Return the view with validation errors and input data
                return View(doctor);
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", doctor.DepartmentId);
            return RedirectToAction(nameof(Viewall));
            //return View(doctor);

        }
        public IActionResult Viewall()
        {
            return View(_context.Doctor.Include(x => x.Department).ToList());
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", doctor.DepartmentId);
            return View(doctor);
        }
        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Speciatly,DepartmentId,First_Name,Last_Name")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var U_doctor = await _context.Doctor.FindAsync(id);
                    U_doctor.Speciatly = doctor.Speciatly;
                    U_doctor.First_Name = doctor.First_Name;
                    U_doctor.Last_Name = doctor.Last_Name;
                    U_doctor.DepartmentId = doctor.DepartmentId;
                    _context.Update(U_doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Viewall));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "DepartmentName", doctor.DepartmentId);
            return RedirectToAction(nameof(Viewall));
        }

        // The missing DoctorExists method added here
        private bool DoctorExists(string id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Viewall)); // Ensure this redirects to your list page
        }

    }

}
