using Tenta_API.Model;
using Tenta_API.ViewModel;
using Tenta_API.ViewModel.Category;
using Tenta_API.ViewModel.Course;

namespace Tenta_API.Interfaces
{
  public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> GetAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseAsync(int id);
        public Task<CourseViewModel?> GetCourseAsync(string title);
        public Task<CourseViewModel?> GetCourseByNumberAsync(int number);
        public Task<CourseWithCategoryViewModel> GetCourseWithCategoryAsync(int id);

        public Task<List<CourseViewModel>> GetCoursesByCategoryAsync(int id);
        public Task<CourseWithInfoViewModel> GetCourseWithInfoAsync(int id);
        public Task<List<CourseWithInfoViewModel>> GetCategoryWithCoursesAndInfoAsync(int id);
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public void DeleteCourse(int id);
        public Task<bool> SaveAllAsync();
    }
}