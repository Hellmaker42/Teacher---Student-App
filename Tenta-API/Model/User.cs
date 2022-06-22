namespace Tenta_API.Model
{
  public class User
  {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public bool StudentOrTeacher { get; set; }
    public Address? Address { get; set; }




    
    // public ICollection<CourseStudent>? CourseStudents { get; set; }
    // public ICollection<Qualification> Qualifications { get; set; } = new List<Qualification>();
  }
}