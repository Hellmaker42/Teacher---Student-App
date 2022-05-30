using Tenta_API.ViewModel.Course;

namespace Tenta_API.ViewModel.Category
{
  public class CategoryWithCoursesViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}