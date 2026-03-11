using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Models;
using Hospitl_Mangement_MVC.ViewModels; // Import the ViewModel namespace
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Hospitl_Mangement_MVC.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IGenericRepository<Doctor> _doctorRepository;
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<Patient> _patientRepository;
        private readonly IGenericRepository<Treatment> _treatmentRepository; // Add Treatment Repository
        private readonly IGenericRepository<Medication> _medicationRepository; // Add Medication Repository
        private readonly IGenericRepository<Prescription> _prescriptionRepository; // Add Prescription Repository

        public DoctorsController(
            IGenericRepository<Doctor> doctorRepository,
            IGenericRepository<Appointment> appointmentRepository,
            IGenericRepository<Patient> patientRepository,
            IGenericRepository<Treatment> treatmentRepository, // Add Treatment Repository
            IGenericRepository<Medication> medicationRepository, // Add Medication Repository
            IGenericRepository<Prescription> prescriptionRepository) // Add Prescription Repository
        {
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _treatmentRepository = treatmentRepository; // Assign the Treatment Repository
            _medicationRepository = medicationRepository; // Assign the Medication Repository
            _prescriptionRepository = prescriptionRepository; // Assign the Prescription Repository
        }

        // Action: List all doctors
        public IActionResult AllDoctors()
        {
            var doctors = _doctorRepository.GetAll().ToList();
            var doctorViewModels = doctors.Select(doctor => new DoctorViewModel
            {
                Id = doctor.Id,
                First_Name = doctor.First_Name,
                Last_Name = doctor.Last_Name,
                Speciatly = doctor.Speciatly,
                DepartmentName = doctor.Department?.DepartmentName
            }).ToList();

            if (!doctorViewModels.Any())
            {
                return Content("No doctors found.");
            }

            return View(doctorViewModels);
        }

        // GET: Display the Create Prescription form
        public IActionResult CreatePrescription(int appointmentId)
        {
            var appointment = _appointmentRepository.GetById(appointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            // Fetch available treatments and medications
            var treatments = _treatmentRepository.GetAll();
            var medications = _medicationRepository.GetAll();

            var viewModel = new CreatePrescriptionViewModel
            {
                Treatments = treatments.Select(t => new SelectListItem
                {
                    Value = t.TreatmentId.ToString(),
                    Text = t.Diagnosis // or another descriptive field
                }),
                Medications = medications.Select(m => new SelectListItem
                {
                    Value = m.MedicationID.ToString(),
                    Text = m.MedicationName
                }),
                AppointmentId = appointmentId // Pass the appointment ID for later use
            };

            return View(viewModel);
        }

        // POST: Handle the creation of the prescription
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePrescription(CreatePrescriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var prescription = new Prescription
                {
                    TreatmentID = model.TreatmentId,
                    MedicationID = model.MedicationId,
                    Quantity = model.Quantity,
                    Duration = model.Duration
                    // Add any other necessary properties
                };

                // Save the prescription using your repository
                _prescriptionRepository.Add(prescription);

                return RedirectToAction("Index", "Appointments"); // Redirect to a relevant page
            }

            // If validation fails, repopulate the dropdowns
            var treatments = _treatmentRepository.GetAll();
            var medications = _medicationRepository.GetAll();

            model.Treatments = treatments.Select(t => new SelectListItem
            {
                Value = t.TreatmentId.ToString(),
                Text = t.Diagnosis
            });
            model.Medications = medications.Select(m => new SelectListItem
            {
                Value = m.MedicationID.ToString(),
                Text = m.MedicationName
            });

            return View(model);
        }
    }
}
