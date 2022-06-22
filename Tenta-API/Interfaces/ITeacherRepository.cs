using Tenta_API.ViewModel.User;

namespace Tenta_API.Interfaces
{
  public interface ITeacherRepository
  {
    public Task AddTeacherAsync(PostUserViewModel teachModel);
    public void DeleteTeacher(int id);
    public Task<bool> SaveAllAsync();
  }
}