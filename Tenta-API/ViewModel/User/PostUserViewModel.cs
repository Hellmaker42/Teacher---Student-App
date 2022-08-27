using Tenta_API.ViewModel.Course;

namespace Tenta_API.ViewModel.User
{
  public class PostUserViewModel
  {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool StudentOrTeacher { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public List<PostCourseViewModel>? Courses { get; set; }
  }
}