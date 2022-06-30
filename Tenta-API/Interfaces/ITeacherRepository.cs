using Tenta_API.ViewModel.User;

namespace Tenta_API.Interfaces
{
  public interface ITeacherRepository
  {
    public Task AddTeacherAsync(PostUserViewModel teachModel);
    public Task<List<UserViewModel>> GetAllTeachersAsync();
    public Task<UserViewModel> GetTeacherByIdAsync(int id);
    public Task UpdateTeacherAsync(int id, PostUserViewModel teacherModel);
    public Task DeleteTeacherAsync(int id);
    public Task<bool> SaveAllAsync();
  }
}