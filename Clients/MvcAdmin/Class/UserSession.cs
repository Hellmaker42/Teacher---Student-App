using MvcAdmin.ViewModels;

namespace MvcAdmin.Class
{
  public class UserSession
  {
    public static UserViewModel? User { get; set; }
    public static List<CourseViewModel>? Courses { get; set; } = new List<CourseViewModel>();

    public static void AddCourse(CourseViewModel model)
    {
      Courses!.Add(model);
    }
    public static void RemoveCourse(int id)
    {
      var courseToRemove = Courses!.Single(c => c.CourseId == id);
      Courses!.Remove(courseToRemove);
    }
  }
}