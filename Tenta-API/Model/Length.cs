using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Length
    {
      [Key]
        public int Id { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public ICollection<Course>? Courses { get; set;}
    }
}