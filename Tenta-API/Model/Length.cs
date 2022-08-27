namespace Tenta_API.Model
{
  public class Length
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set;}
    }
}