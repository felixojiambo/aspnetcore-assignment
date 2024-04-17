using System.ComponentModel.DataAnnotations;

namespace Queue_Management_System.Models
{
    public class ServicePoint
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProviderId { get; set; }
        
    }
}