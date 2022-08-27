using Tenta_API.ViewModel.Address;
using Tenta_API.ViewModel.Course;

namespace Tenta_API.ViewModel.User
{
  public class UserViewModel
    {
      public int UserId { get; set; }
      public string? UserFirstName { get; set; }
      public string? UserLastName { get; set; }
      public string? UserEmail { get; set; }
      public string? UserPhone { get; set; }
      public bool UserStudentOrTeacher { get; set; }
      public AddressViewModel? UserAddress { get; set; }
      public ICollection<CourseViewModel> UserCourses { get; set; } = new List<CourseViewModel>();
    }
}