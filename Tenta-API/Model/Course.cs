using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tenta_API.Model
{
  public class Course
  {
    [Key]
    public int Id { get; set; }
    public int Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public bool IsVideo { get; set; }
    public int CategoryId { get; set; }
    public int LengthId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = new Category();
    [ForeignKey("LengthId")]
    public Length Length { get; set; } = new Length();
    public ICollection<Student> Students { get; set; } = new List<Student>();
  }
}