using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Category
    {
      [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}