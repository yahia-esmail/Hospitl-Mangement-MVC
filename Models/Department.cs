using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitl_Mangement_MVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? DepartmentName { get; set; }
        [Required, MaxLength(100)]
        public string? Location { get; set; }

        [MaxLength(250)]
        public string? Describe { get; set; }

        //[ForeignKey("Staff")]
        public string? StaffId { get; set; }
        public ICollection<Staff>? Staff { get; set; }
        //[ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public ICollection<Doctor>? Doctor { get; set; }
        public byte[]? ImageURL { get; set; }

    }
}
