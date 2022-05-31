using Tenta_API.ViewModel.Student;

namespace Tenta_API.Interfaces
{
  public interface IStudentRepository
  {
    public Task AddStudentAsync(PostStudentViewModel studentModel);
    public void DeleteStudent(int id);
    public Task<bool> SaveAllAsync();

  }
}