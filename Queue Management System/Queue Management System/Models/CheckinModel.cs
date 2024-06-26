namespace Queue_Management_System.Models
{
    public class CheckinModel
    {
        public int SelectedServiceId { get; set; } // The ID of the selected service
         public int ServiceTypeId { get; set; }
        public string? CustomerName { get; set; }
        public List<Service> AvailableServices { get; set; } = new List<Service>();
    }
}
