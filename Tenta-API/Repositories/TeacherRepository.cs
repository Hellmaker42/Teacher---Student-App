using AutoMapper;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.Teacher;

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

    public async Task AddTeacherAsync(PostTeacherViewModel teachModel)
    {
      var addressToAdd = new Address();
      addressToAdd.Street = teachModel.Street;
      addressToAdd.Number = teachModel.Number;
      addressToAdd.Zipcode = teachModel.Zipcode;
      addressToAdd.City = teachModel.City;

      var teacherToAdd = new Teacher();
      teacherToAdd.FirstName = teachModel.FirstName;
      teacherToAdd.LastName = teachModel.LastName;
      teacherToAdd.Email = teachModel.Email;
      teacherToAdd.Phone = teachModel.Phone;



      await _context.Addresses.AddAsync(addressToAdd);
      await _addressRepo.SaveAllAsync();
      teacherToAdd.AddressId = _context.Addresses.OrderBy(c => c.Id).Last().Id;

      await _context.Teachers.AddAsync(teacherToAdd);
    }
    public void DeleteTeacher(int id)
    {
      var teacher = _context.Teachers.Find(id);

      if (teacher is not null)
      {
        _addressRepo.DeleteAddress(teacher.AddressId);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}