using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitl_Mangement_MVC.Models
{
    public class Doctor : BaseEntity
    {
        public string? Speciatly { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Appointment>? Appointment { get; set; }
    }
}
