using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Qualification
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}