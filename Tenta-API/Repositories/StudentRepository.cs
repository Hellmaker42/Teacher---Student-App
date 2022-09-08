using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.Address;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Repositories
{
  public class StudentRepository : IStudentRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    private readonly IAddressRepository _addressRepo;

    public StudentRepository(CourseContext context, IMapper mapper, IAddressRepository addressRepo)
    {
      _addressRepo = addressRepo;
      _context = context;
      _mapper = mapper;
    }

    public async Task AddStudentAsync(PostUserViewModel studentModel)
    {
      var userToAdd = new User();
      userToAdd.FirstName = studentModel.FirstName;
      userToAdd.LastName = studentModel.LastName;
      userToAdd.Email = studentModel.Email;
      userToAdd.Phone = studentModel.Phone;
      userToAdd.StudentOrTeacher = studentModel.StudentOrTeacher;
      userToAdd.Address = new Address
      {
        Street = studentModel.Street,
        Number = studentModel.Number,
        Zipcode = studentModel.Zipcode,
        City = studentModel.City
      };
      await _context.Users.AddAsync(userToAdd);
    }

    public async Task<List<UserViewModel>> GetAllStudentsAsync()
    {
      return await _context.Users.ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).Where(u => u.UserStudentOrTeacher == false).ToListAsync();
    }

    public async Task<UserViewModel> GetStudentByIdAsync(int id)
    {
      return await _context.Users.Include(u => u.Address).Where(a => a.Address!.Id == a.Id).Where(u => u.Id == id).ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();
    }

    public async Task<UserViewModel> GetStudentByEmailAsync(string email)
    {
      return await _context.Users.Where(u => u.Email!.ToLower() == email.ToLower()).ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();
    }
    public async Task<bool> CheckEmailAsync(string email)
    {
      var user = await _context.Users.Where(u => u.Email!.ToLower() == email!.ToLower()).FirstOrDefaultAsync();
      if(user is not null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public async Task UpdateStudentAsync(int id, PostUserViewModel studentModel)
    {
      var oldStudent = await _context.Users.FindAsync(id);

      if (oldStudent is not null)
      {
        oldStudent.FirstName = studentModel.FirstName;
        oldStudent.LastName = studentModel.LastName;
        oldStudent.Email = studentModel.Email;
        oldStudent.Phone = studentModel.Phone;
        oldStudent.StudentOrTeacher = studentModel.StudentOrTeacher;
        _context.Update(oldStudent);

        var oldAddress = await _context.Addresses.FindAsync(id);

        if (oldAddress is not null)
        {
          oldAddress.Street = studentModel.Street;
          oldAddress.Number = studentModel.Number;
          oldAddress.Zipcode = studentModel.Zipcode;
          oldAddress.City = studentModel.City;
          _context.Update(oldAddress);
        }
      }
      else
      {
        throw new Exception($"Vi kunde inte hitta någon lärare med id: {id}");
      }
    }

    public async Task AddCourseToStudentAsync(AddCourseToStudentViewModel model)
    {

      User user = await _context.Users.FirstAsync(x => x.Email!.ToLower() == model.StudentEmail!.ToLower());
      Course course = await _context.Courses.FirstAsync(x => x.Id == model.CourseId);

      user.Course!.Add(course);
      course.User!.Add(user);

      _context.Users.Update(user);
      _context.Courses.Update(course);
    }

    public async Task RemoveCourseFromStudentAsync(RemoveCourseFromStudentViewModel model)
    {
      User user = await _context.Users.FirstAsync(x => x.Email!.ToLower() == model.StudentEmail!.ToLower());
      Course course = await _context.Courses.FirstAsync(x => x.Id == model.CourseId); 

        var courseWithUser = await _context.Courses.Include(c => c.User!.Where(u => u.Id == user.Id)).FirstOrDefaultAsync(c => c.Id == model.CourseId);
        courseWithUser!.User!.Remove(user);

        _context.ChangeTracker.DetectChanges();
        // await _context.SaveChangesAsync();   

      // user.Course!.Remove(course);
      // course.User!.Remove(user);  

      // _context.Users.Update(user);
      // _context.Courses.Update(course); 
    }

    public async Task DeleteStudentAsync(int id)
    {
      var student = await _context.Users.FindAsync(id);

      if (student is not null)
      {
        _context.Users.Remove(student);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}