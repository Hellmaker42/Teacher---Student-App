using Tenta_API.ViewModel.Teacher;

namespace Tenta_API.Interfaces
{
  public interface ITeacherRepository
  {
    public Task AddTeacherAsync(PostTeacherViewModel teachModel);
    public void DeleteTeacher(int id);
    public Task<bool> SaveAllAsync();
  }
}