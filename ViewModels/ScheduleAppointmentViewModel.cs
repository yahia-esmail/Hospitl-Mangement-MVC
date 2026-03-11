using Microsoft.AspNetCore.Mvc.Rendering;
using Hospitl_Mangement_MVC.ViewModels;

namespace Hospitl_Mangement_MVC.ViewModels
{
    public class ScheduleAppointmentViewModel
    {
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ReasonForVisit { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }
    }
}
