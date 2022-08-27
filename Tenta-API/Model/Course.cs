namespace Tenta_API.Model
{
  public class Course
  {
    public int Id { get; set; }
    public int Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public bool IsVideo { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; } = new Category();
    public Length? Length { get; set; } = new Length();
    public ICollection<User>? User { get; set; } = new List<User>();
  }
}