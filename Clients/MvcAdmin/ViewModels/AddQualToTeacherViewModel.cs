namespace MvcAdmin.ViewModels
{
  public class AddQualToTeacherViewModel
    {
        public List<int>? CourseIds { get; set; } = new List<int>();
        public string? TeacherEmail { get; set; }
    }
}