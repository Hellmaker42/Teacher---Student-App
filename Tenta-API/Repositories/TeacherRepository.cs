using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Repositories
{
  public class TeacherRepository : ITeacherRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    private readonly IAddressRepository _addressRepo;
    public TeacherRepository(CourseContext context, IMapper mapper, IAddressRepository addressRepo)
    {
      _addressRepo = addressRepo;
      _mapper = mapper;
      _context = context;
    }

    public async Task AddTeacherAsync(PostUserViewModel teachModel)
    {
      var teacherToAdd = new User();
      teacherToAdd.FirstName = teachModel.FirstName;
      teacherToAdd.LastName = teachModel.LastName;
      teacherToAdd.Email = teachModel.Email;
      teacherToAdd.Phone = teachModel.Phone;
      teacherToAdd.StudentOrTeacher = teachModel.StudentOrTeacher;
      teacherToAdd.Address = new Address
      {
        Street = teachModel.Street,
        Number = teachModel.Number,
        Zipcode = teachModel.Zipcode,
        City = teachModel.City
      };

      await _context.Users.AddAsync(teacherToAdd);
    }

    public async Task<List<UserViewModel>> GetAllTeachersAsync()
    {
      return await _context.Users.Include(u => u.Address)
        .Where(a => a.Address!.Id == a.Id)
        .Where(u => u.StudentOrTeacher == true)
        .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }
    public async Task<UserViewModel> GetTeacherByIdAsync(int id)
    {
      return await _context.Users.Include(u => u.Address)
        .Where(a => a.Address!.Id == a.Id)
        .Where(u => u.Id == id)
        .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();
    }

    public async Task<UserViewModel> GetTeacherByEmailAsync(string email)
    {
      return await _context.Users
        .Where(u => u.Email!.ToLower() == email.ToLower())
        .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();
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

    public async Task<int> GetLastCreatedTeacherAsync()
    {
      var lastTeacher = await _context.Users.Where(u => u.StudentOrTeacher == true).LastAsync();
      Console.WriteLine(lastTeacher);
      int teacherId = lastTeacher.Id;
      return teacherId;
    }

    public async Task UpdateTeacherAsync(int id, PostUserViewModel teacherModel)
    {
      var oldTeacher = await _context.Users.FindAsync(id);

      if (oldTeacher is not null)
      {
        oldTeacher.FirstName = teacherModel.FirstName;
        oldTeacher.LastName = teacherModel.LastName;
        oldTeacher.Email = teacherModel.Email;
        oldTeacher.Phone = teacherModel.Phone;
        oldTeacher.StudentOrTeacher = teacherModel.StudentOrTeacher;
        _context.Update(oldTeacher);

        var oldAddress = await _context.Addresses.FindAsync(id);

        if (oldAddress is not null)
        {
          oldAddress.Street = teacherModel.Street;
          oldAddress.Number = teacherModel.Number;
          oldAddress.Zipcode = teacherModel.Zipcode;
          oldAddress.City = teacherModel.City;
          _context.Update(oldAddress);
        }
      }
      else
      {
        throw new Exception($"Vi kunde inte hitta någon lärare med id: {id}");
      }


      // var teacherToUpdate = new User();
      // teacherToUpdate.FirstName = teacherModel.FirstName;
      // teacherToUpdate.LastName = teacherModel.LastName;
      // teacherToUpdate.Email = teacherModel.Email;
      // teacherToUpdate.Phone = teacherModel.Phone;
      // teacherToUpdate.StudentOrTeacher = teacherModel.StudentOrTeacher;
      // teacherToUpdate.Address = new Address{
      //   Street = teacherModel.Street,
      //   Number = teacherModel.Number,
      //   Zipcode = teacherModel.Zipcode,
      //   City = teacherModel.City
      // };

    }

    public async Task UpdateQualToTeacherAsync(AddQualToTeacherViewModel qualModel)
    {
      User user = await _context.Users.FirstAsync(u => u.Email!.ToLower() == qualModel.TeacherEmail!.ToLower());  
      Course course = new();


      for (int i = 0; i < qualModel.CourseIds!.Count; i++)
      {
        course = await _context.Courses.FirstAsync(c => c.Id == qualModel.CourseIds[i]);
          
        user.Course!.Add(course);
        course.User!.Add(user);
      }

      _context.Users.Update(user);
      _context.Courses.Update(course);
    }


    // public async Task UpdateQualToTeacherFromEditAsync(AddQualToTeacherViewModel qualModel)
    // {
    //   UserViewModel userModel = await _context.Users
    //     .Where(u => u.Email!.ToLower() == qualModel.TeacherEmail!.ToLower())
    //     .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();

    //   User user = await _context.Users.FirstAsync(u => u.Email == qualModel.TeacherEmail);

    //   foreach (var course in userModel.UserCourses)
    //   {
    //     await _context.Courses.Remove(course);
    //     await _context.SaveChangesAsync();
    //   }
    // }

    public async Task UpdateQualToTeacherFromEditAsync(AddQualToTeacherViewModel qualModel)
    {
      UserViewModel userModel = await _context.Users
        .Where(u => u.Email!.ToLower() == qualModel.TeacherEmail!.ToLower())
        .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider).FirstAsync();

      var user = await _context.Users.FirstAsync(u => u.Email == qualModel.TeacherEmail);
      Course newCourse = new();

      foreach (var course in userModel.UserCourses)
      {
        var courseWithUser = await _context.Courses.Include(c => c.User!.Where(u => u.Id == user.Id)).FirstOrDefaultAsync(c => c.Id == course.CourseId);
        courseWithUser!.User!.Remove(user);
        _context.ChangeTracker.DetectChanges();
        await _context.SaveChangesAsync();      
      }

      if(qualModel.CourseIds!.Count > 0)
      {
        for (int i = 0; i < qualModel.CourseIds!.Count; i++)
        {
          newCourse = await _context.Courses.FirstAsync(c => c.Id == qualModel.CourseIds[i]);
            
          user.Course!.Add(newCourse);
          newCourse.User!.Add(user);
        }

        _context.Users.Update(user);
        _context.Courses.Update(newCourse);
      }
        _context.ChangeTracker.DetectChanges();
    }



    // public async Task AddSingleQualToTeacherAsync(AddSingleQualToTeacherViewModel singleQualModel)
    // {
    //   User user = await _context.Users.FirstAsync(u => u.Email!.ToLower() == singleQualModel.TeacherEmail!.ToLower());
      
    //   Course course = await _context.Users.C




    //   for (int i = 0; i < qualModel.CourseIds!.Count; i++)
    //   {
    //     course = await _context.Courses.FirstAsync(c => c.Id == qualModel.CourseIds[i]);
        
    //     user.Course!.Add(course);
    //     course.User!.Add(user);
    //   }

    //   _context.Users.Update(user);
    //   _context.Courses.Update(course);
    // }

    public async Task DeleteTeacherAsync(int id)
    {
      var teacher = await _context.Users.FindAsync(id);

      if (teacher is not null)
      {
        _context.Users.Remove(teacher);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}