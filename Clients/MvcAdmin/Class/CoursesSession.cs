using MvcAdmin.ViewModels;

namespace MvcAdmin.Class
{
  public class CoursesSession
  {
    public static List<CourseViewModel>? Courses { get; set; } = new List<CourseViewModel>();

    public static void RemoveCourse(int id)
    {
      var courseToRemove = Courses!.Single(c => c.CourseId == id);
      Courses!.Remove(courseToRemove);
    }
    public static void AddCourse(CourseViewModel model)
    {
      Courses!.Add(model);
    }
  }
}