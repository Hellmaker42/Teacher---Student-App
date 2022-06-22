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
  public class UserRepository : IUserRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    private readonly IAddressRepository _addressRepo;

    public UserRepository(CourseContext context, IMapper mapper, IAddressRepository addressRepo)
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
      await _context.SaveChangesAsync();

      // var addressToAdd = new Address();
      // addressToAdd.Street = studentModel.Street;
      // addressToAdd.Number = studentModel.Number;
      // addressToAdd.Zipcode = studentModel.Zipcode;
      // addressToAdd.City = studentModel.City;
      // addressToAdd.User = new User
      // {
      //   FirstName = studentModel.FirstName,
      //   LastName = studentModel.LastName,
      //   Email = studentModel.Email,
      //   Phone = studentModel.Phone,
      //   StudentOrTeacher = studentModel.StudentOrTeacher
      // };

      // await _context.Addresses.AddAsync(addressToAdd);
      // await _context.SaveChangesAsync();

    }

    public async Task<List<UserViewModel>> GetAllStudentsAsync()
    {
      return await _context.Users.ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    // public async Task AddCourseToStudentAsync(AddCourseToStudentViewModel model)
    // {

    //   User user = _context.Users.First(x => x.Email!.ToLower() == model.StudentEmail!.ToLower());

    //   CourseStudent courseStudent = new CourseStudent{
    //     CourseId = model.CourseId,
    //     UserId = user.Id
    //   };
    //   await _context.CourseStudent.AddAsync(courseStudent);
    // }

    public void DeleteStudent(int id)
    {
      var student = _context.Users.Find(id);

      if(student is not null)
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