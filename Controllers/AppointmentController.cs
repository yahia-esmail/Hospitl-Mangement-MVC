using Microsoft.AspNetCore.Mvc;
using Hospitl_Mangement_MVC.Interface;
using Hospitl_Mangement_MVC.ViewModels;
using System.Linq;
using Hospitl_Mangement_MVC.Models;

namespace Hospitl_Mangement_MVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IGenericRepository<Appointment> _appointmentRepository;
        private readonly IGenericRepository<Patient> _patientRepository;

        public AppointmentController(IGenericRepository<Appointment> appointmentRepository, IGenericRepository<Patient> patientRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        // Action to display the list of appointments for a doctor
        public IActionResult Index(string doctorId)
        {
            var appointments = _appointmentRepository.GetAll()
                .Where(a => a.DoctorId == doctorId)
                .Select(a => new AppointmentViewModel
                {
                    AppointmentId = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    PatientName = a.Patient != null
                        ? a.Patient.First_Name + " " + a.Patient.Last_Name
                        : "No Patient Assigned", // Handle null case
                    Status = a.Status,
                    Reason = a.Reason
                }).ToList();

            return View(appointments);
        }


        // Action to schedule a new appointment for a doctor
        public IActionResult Schedule(string doctorId)
        {
            var patients = _patientRepository.GetAll();

            var viewModel = new ScheduleAppointmentViewModel
            {
                DoctorId = doctorId,
                Patients = patients.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = p.Id,
                    Text = p.First_Name + " " + p.Last_Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Schedule(ScheduleAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    AppointmentDate = model.AppointmentDate,
                    Status = "Scheduled",
                    Reason = model.ReasonForVisit,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId
                };

                _appointmentRepository.Add(appointment);
                return RedirectToAction("Index", new { doctorId = model.DoctorId });
            }

            // If validation fails, reload patients list
            var patients = _patientRepository.GetAll();
            model.Patients = patients.Select(p => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = p.Id,
                Text = p.First_Name + " " + p.Last_Name
            }).ToList();

            return View(model);
        }
    }
}
