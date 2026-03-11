using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospitl_Mangement_MVC.ViewModels
{
    public class CreatePrescriptionViewModel
    {
        public int AppointmentId { get; set; } // Add this line to include AppointmentId

        [Required]
        public int TreatmentId { get; set; }

        [Required]
        public int MedicationId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Duration { get; set; }

        public IEnumerable<SelectListItem> Treatments { get; set; }
        public IEnumerable<SelectListItem> Medications { get; set; }
    }
}
