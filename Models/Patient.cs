using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitl_Mangement_MVC.Models
{
    public class Patient : BaseEntity
    {
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Emergancy_Contact { get; set; }
        public string? Birthdate { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

    }
}
