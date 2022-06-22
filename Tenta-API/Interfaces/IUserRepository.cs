using Tenta_API.ViewModel.Address;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Interfaces
{
  public interface IUserRepository
  {
    public Task AddStudentAsync(PostUserViewModel studentModel);
    public Task<List<UserViewModel>> GetAllStudentsAsync();
    // public Task AddCourseToStudentAsync(AddCourseToStudentViewModel studentCourse);
    // public void DeleteStudent(int id);
    public Task<bool> SaveAllAsync();
  }
}