using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospitl_Mangement_MVC.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }



        public string Diagnosis { get; set; }
        public string TreatmentDescription { get; set; }
        public DateTime SartDate { get; set; }

        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(Patient))]
        public string PatientID { get; set; }
        public  Patient Patient { get; set; }

        [ForeignKey(nameof(Doctor))]

        public string DoctorID { get; set; }
        public  Doctor Doctor { get; set; }

    }
}
