using Tenta_API.ViewModel.Address;

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

      // public AddressViewModel UserAddress { get; set; } = new AddressViewModel();
      // public ICollection<CourseViewModel> StudentCourses { get; set; } = new List<CourseViewModel>();
    }
}