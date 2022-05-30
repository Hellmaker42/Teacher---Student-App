namespace Tenta_API.ViewModel.Course
{
  public class CourseWithCategoryViewModel
  {
    public int CourseId { get; set; }
    public int CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseDescription { get; set; }
    public string? CourseDetails { get; set; }
    public bool CourseIsVideo { get; set; }
    public string? CourseVideoDescription { get; set; }
    public string? CategoryName { get; set; }

  }
}