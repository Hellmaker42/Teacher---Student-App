using System.ComponentModel.DataAnnotations;

namespace Tenta_API.Model
{
  public class Qualification
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        // public ICollection<User> Users { get; set; } = new List<User>();
    }
}