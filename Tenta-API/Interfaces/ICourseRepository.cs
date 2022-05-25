using Tenta_API.Model;
using Tenta_API.ViewModel;

namespace Tenta_API.Interfaces
{
  public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> GetAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseAsync(int id);
        public Task<CourseViewModel?> GetCourseAsync(string title);
        public Task<List<CourseViewModel>> GetCourseByCategoryAsync(string category);
        public Task AddCourseAsync(Course model);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public void DeleteCourse(int id);
        public Task<bool> SaveAllAsync();
    }
}