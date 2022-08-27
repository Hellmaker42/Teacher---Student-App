namespace MvcAdmin.ViewModels
{
  public class CourseViewModel
    {
    public int CourseId { get; set; }
    public int CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseDescription { get; set; }
    public string? CourseDetails { get; set; }
    public bool CourseIsVideo { get; set; }
    public int CourseCategoryId { get; set; }
    public CategoryViewModel? CourseCategory { get; set; }
    public LengthViewModel? CourseLength { get; set; }   
    }
}