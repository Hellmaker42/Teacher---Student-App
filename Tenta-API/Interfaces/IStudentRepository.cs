using Tenta_API.ViewModel.User;

namespace Tenta_API.Interfaces
{
  public interface IStudentRepository
  {
    public Task AddStudentAsync(PostUserViewModel studentModel);
    public Task<List<UserViewModel>> GetAllStudentsAsync();
    public Task<UserViewModel> GetStudentByIdAsync(int id);
    public Task<UserViewModel> GetStudentByEmailAsync(string email);
    public Task AddCourseToStudentAsync(AddCourseToStudentViewModel studentCourse);
    public Task RemoveCourseFromStudentAsync(RemoveCourseFromStudentViewModel studentCourse);
    public Task UpdateStudentAsync(int id, PostUserViewModel teacherModel);
    public Task<bool> CheckEmailAsync(string email);
    public Task DeleteStudentAsync(int id);
    public Task<bool> SaveAllAsync();

  }
}