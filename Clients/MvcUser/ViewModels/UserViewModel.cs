using System.ComponentModel.DataAnnotations.Schema;

namespace MvcUser.ViewModels
{
  public class UserViewModel
  {
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPhone { get; set; }
    [ForeignKey("AddressId")]
    public int UserAddressId { get; set; }
    // public AddressViewModel UserAddress { get; set; } = new AddressViewModel();
    public bool UserStudentOrTeacher { get; set; }
    public ICollection<CourseViewModel> StudentCourses { get; set; } = new List<CourseViewModel>();
    // public ICollection<> MyProperty { get; set; }
  }
}
