using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospitl_Mangement_MVC.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionID { get; set; }

        [ForeignKey(nameof(Treatment))]
        public int TreatmentID { get; set; }

        [ForeignKey(nameof(Medication))]
        public int MedicationID { get; set; }
        public int Quantity { get; set; }
        public int Duration { get; set; }

        public ICollection<Medication> Medications { get; set; }

    }
}
