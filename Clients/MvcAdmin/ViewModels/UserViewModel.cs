namespace MvcAdmin.ViewModels
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
    public List<CourseViewModel>? UserCourses { get; set; }
  }
}