using System.ComponentModel.DataAnnotations;

namespace Hospitl_Mangement_MVC.ViewModels
{
    public class DoctorViewModel
    {
        public string Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Speciatly { get; set; }
        public string DepartmentName { get; set; } // For displaying department name

       
        public byte[]? ProfilePicture { get; set; }
    }
}
    