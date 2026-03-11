using Hospitl_Mangement_MVC.Data;
using Hospitl_Mangement_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Net.Mail;

namespace Hospitl_Mangement_MVC.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly HospitalDbContext _context;
        private readonly UserManager<BaseEntity> _userManager;
        private readonly EmailService _emailService;


        public IActionResult Index()
        {
            return View();
        }
        public PatientController(HospitalDbContext context, UserManager<BaseEntity> userManager, EmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        // GET: Appointment
        public ActionResult MakeAppointment()
        {
            // Simulate fetching available doctors from database or service
            ViewBag.Doctor = _context.Doctor.ToList();

            return View();
        }

        // POST: Appointment/Submit
        [HttpPost]
        public async Task<IActionResult> Submit(Appointment appointment)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Get the currently logged-in user
                var user = await _userManager.GetUserAsync(User);

                // If you want to associate the appointment with the logged-in user
                //appointment.PatientId = user?.Id; // Assuming Appointment has a PatientId property

                // Add the appointment to the database
                _context.Add(appointment);
                await _context.SaveChangesAsync(); // Save changes asynchronously

                // Dynamically send an email to the logged-in user's email address
                string recipientEmail = user?.Email; // Get the user's email from the user object
                string emailSubject = "Confirm Appointment";
                string emailBody = "Hello " + user?.First_Name + ", you have now successfully made an appointment";

                // Send email to the user's email address
                if (!string.IsNullOrEmpty(recipientEmail))
                {
                    await _emailService.SendEmailAsync(recipientEmail, emailSubject, emailBody);
                }

                // Store success message in TempData to display after redirection
                TempData["SuccessMessage"] = "Your appointment request has been sent successfully.";

                return RedirectToAction("MakeAppointment");
            }

            // If the model is invalid, reload the form with errors and available doctors
            ViewBag.Doctor = _context.Doctor.ToList();
            return View("MakeAppointment");
        }



        private List<Doctor> GetDoctors()
        {
            // Simulated list of doctors, replace with actual database call
            return new List<Doctor>
            {

            };
        }

        public ActionResult AppointmentConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewPrescription(int prescriptionId)
        {
            // Fetch the prescription with the related treatment and medication details
            var prescription = _context.Prescriptions
                                       .Include(p => p.Medications) // Include the related medications
                                       .FirstOrDefault(p => p.PrescriptionID == prescriptionId);

            // Check if the prescription exists
            if (prescription == null)
            {
                return NotFound("Prescription not found.");
            }

            // Pass the prescription data to the view
            return View(ViewPrescription);
        }

    }
}
