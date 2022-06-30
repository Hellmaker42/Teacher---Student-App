using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcUser.ViewModels
{
  public class StudentViewModel
  {
    public string? StudentFirstName { get; set; }
    public string? StudentLastName { get; set; }
    public string? StudentEmail { get; set; }
    public string? StudentPhone { get; set; }
    public int StudentAddressId { get; set; }
    [ForeignKey("AddressId")]
    // public AddressViewModel StudentAddress { get; set; } = new AddressViewModel();
    public ICollection<CourseViewModel> StudentCourses { get; set; } = new List<CourseViewModel>();
  }
}