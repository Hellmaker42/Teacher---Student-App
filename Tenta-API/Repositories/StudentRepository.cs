using AutoMapper;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.Student;

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

    public async Task AddStudentAsync(PostStudentViewModel studentModel)
    {
      var addressToAdd = new Address();
      addressToAdd.Street = studentModel.Street;
      addressToAdd.Number = studentModel.Number;
      addressToAdd.Zipcode = studentModel.Zipcode;
      addressToAdd.City = studentModel.City;

      var studentToAdd = new Student();
      studentToAdd.FirstName = studentModel.FirstName;
      studentToAdd.LastName = studentModel.LastName;
      studentToAdd.Email = studentModel.Email;
      studentToAdd.Phone = studentModel.Phone;
   


      await _context.Addresses.AddAsync(addressToAdd);
      await _addressRepo.SaveAllAsync();
      studentToAdd.AddressId = _context.Addresses.OrderBy(c => c.Id).Last().Id;

      await _context.Students.AddAsync(studentToAdd);





      // var studentToAdd = _mapper.Map<Student>(studentModel);
      // var PostAddressToAdd = studentModel.Address;
      // PostAddressToAdd.Street = studentModel.Address.Street;
      // PostAddressToAdd.Number = studentModel.Address.Number;
      // PostAddressToAdd.Zipcode = studentModel.Address.Zipcode;
      // PostAddressToAdd.City = studentModel.Address.City;

      // var addressToAdd = _mapper.Map<Address>(PostAddressToAdd);

      // await _context.Students.AddAsync(studentToAdd);
      // await _context.Addresses.AddAsync(addressToAdd);

    }

    public void DeleteStudent(int id)
    {
      var student = _context.Students.Find(id);

      if(student is not null)
      {
        _addressRepo.DeleteAddress(student.AddressId);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}