using System.ComponentModel.DataAnnotations.Schema;
using Tenta_API.ViewModel.Address;
using Tenta_API.ViewModel.Course;

namespace Tenta_API.ViewModel.Student
{
  public class StudentViewModel
    {
      public string? StudentFirstName { get; set; }
      public string? StudentLastName { get; set; }
      public string? StudentEmail { get; set; }
      public string? StudentPhone { get; set; }
      public int StudentAddressId { get; set; }
      [ForeignKey("AddressId")]
      public AddressViewModel StudentAddress { get; set; } = new AddressViewModel();
      public ICollection<CourseViewModel> StudentCourses { get; set; } = new List<CourseViewModel>();
    }
}