using System.ComponentModel.DataAnnotations;

namespace Queue_Management_System.Models
{
  public class ServiceType
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
}

}