using System.ComponentModel.DataAnnotations;

namespace Queue_Management_System.Models
{
    public class Ticket
    {
          [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServicePointId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}