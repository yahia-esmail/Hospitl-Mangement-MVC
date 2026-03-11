using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.Models;
using Hospitl_Mangement_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Hospitl_Mangement_MVC.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IGenericRepository<Treatment> _treatmentRepository;
        private readonly IGenericRepository<Medication> _medicationRepository;
        private readonly IGenericRepository<Prescription> _prescriptionRepository;

        public PrescriptionController(
            IGenericRepository<Treatment> treatmentRepository,
            IGenericRepository<Medication> medicationRepository,
            IGenericRepository<Prescription> prescriptionRepository)
        {
            _treatmentRepository = treatmentRepository;
            _medicationRepository = medicationRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        // GET: Display form for creating a prescription
        public IActionResult CreatePrescription()
        {
            var viewModel = new CreatePrescriptionViewModel
            {
                //TreatmentId = treatmentId,  // Pre-populate the treatmentId
                Treatments = _treatmentRepository.GetAll().Select(t => new SelectListItem
                {
                    Value = t.TreatmentId.ToString(),
                    Text = t.TreatmentDescription
                }).ToList(),

                Medications = _medicationRepository.GetAll().Select(m => new SelectListItem
                {
                    Value = m.MedicationID.ToString(),
                    Text = m.MedicationName
                }).ToList()
            };

            return View(viewModel);
        }


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
                    // Set other properties as necessary
                };

                _prescriptionRepository.Add(prescription); // Assuming you have a repository for prescriptions
                return RedirectToAction("Index", "Appointment"); // Redirect back to the appointment list or any other appropriate action
            }

            // If model state is not valid, return to the same view with the current model to display errors
            return View(model);
        }

    }
}
