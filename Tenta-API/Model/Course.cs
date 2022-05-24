using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Course
    {
      [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string? Title { get; set; }
        public int Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}