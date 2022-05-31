using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Teacher
  {
    [Key]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public int AddressId { get; set; }
    public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();

  }
}