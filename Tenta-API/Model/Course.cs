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
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; } = new Category();
    [ForeignKey("LengthId")]
    public int LengthId { get; set; }
    public Length? Length { get; set; } = new Length();
    // public ICollection<CourseStudent>? CourseStudents { get; set; }
  }
}