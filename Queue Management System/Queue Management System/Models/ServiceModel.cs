using System.ComponentModel.DataAnnotations;

namespace Queue_Management_System.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public int ServiceTypeId { get; set; } 
        // Navigation property
        public ServiceType? ServiceType { get; set; }

        internal static void AddScoped<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
}
