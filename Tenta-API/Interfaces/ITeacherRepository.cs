using Tenta_API.ViewModel.User;

namespace Tenta_API.Interfaces
{
  public interface ITeacherRepository
  {
    public Task AddTeacherAsync(PostUserViewModel teachModel);
    public Task<List<UserViewModel>> GetAllTeachersAsync();
    public Task<UserViewModel> GetTeacherByIdAsync(int id);
    public Task<UserViewModel> GetTeacherByEmailAsync(string email);
    public Task<bool> CheckEmailAsync(string email);
    public Task<int> GetLastCreatedTeacherAsync();
    public Task UpdateTeacherAsync(int id, PostUserViewModel teacherModel);
    public Task UpdateQualToTeacherAsync(AddQualToTeacherViewModel qualModel);
    public Task UpdateQualToTeacherFromEditAsync(AddQualToTeacherViewModel qualModel);
    public Task DeleteTeacherAsync(int id);
    public Task<bool> SaveAllAsync();
  }
}