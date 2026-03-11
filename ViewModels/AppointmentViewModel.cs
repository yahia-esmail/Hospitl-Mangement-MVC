namespace Hospitl_Mangement_MVC.ViewModels
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string? PatientName { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }

        // Add this property to hold the Treatment ID
        public int TreatmentId { get; set; }
    }
}
