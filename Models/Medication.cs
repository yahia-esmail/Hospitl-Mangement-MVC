using System.ComponentModel.DataAnnotations;

namespace Hospitl_Mangement_MVC.Models
{
    public class Medication

    {
        [Key]
        public int MedicationID { get; set; }

        [Required]
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequenccy { get; set; }
        public string SideEffectes { get; set; }

    }
}
