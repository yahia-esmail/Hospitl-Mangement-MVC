using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitl_Mangement_MVC.Models
{
    public class Staff :BaseEntity
    {
        public string? RoleId { get; set; }
        public IdentityRole? Role { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
