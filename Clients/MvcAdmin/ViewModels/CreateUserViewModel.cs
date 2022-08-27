namespace MvcAdmin.ViewModels
{
  public class CreateUserViewModel
  {
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = String.Empty;
    public string? Phone { get; set; }
    public bool StudentOrTeacher { get; set; }
    public string? Street { get; set; }
    public int Number { get; set; }
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public ICollection<CourseViewModel>? Course { get; set; } 
  }
}