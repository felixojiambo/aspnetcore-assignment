using System.ComponentModel.DataAnnotations;

namespace Queue_Management_System.Models
{
    public class Customer
    {
         [Key]
        public int Id { get; set; }

        [Required] 
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? ServiceType { get; set; }
    }
}
