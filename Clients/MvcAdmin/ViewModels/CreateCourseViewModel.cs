namespace MvcAdmin.ViewModels
{
  public class CreateCourseViewModel
  {
    public int Number { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public bool IsVideo { get; set; }
    public int CategoryId { get; set; }
    public CreateCategoryViewModel? Category { get; set; } = new CreateCategoryViewModel();
    public CreateLengthViewModel? Length { get; set; } = new CreateLengthViewModel();
  }
}