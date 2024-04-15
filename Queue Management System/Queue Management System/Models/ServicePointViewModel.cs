using System.Collections.Generic;
using Queue_Management_System.Models;

namespace Queue_Management_System.ViewModels
{
    public class ServicePointViewModel
    {
        public List<ServicePoint> ServicePoints { get; set; }
        public ServicePoint SelectedServicePoint { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
