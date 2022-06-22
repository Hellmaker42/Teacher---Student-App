// using AutoMapper;
// using Tenta_API.Data;
// using Tenta_API.Interfaces;
// using Tenta_API.Model;
// using Tenta_API.ViewModel.User;

// namespace Tenta_API.Repositories
// {
//   public class TeacherRepository : IUserRepository
//   {
//     private readonly CourseContext _context;
//     private readonly IMapper _mapper;
//     private readonly IAddressRepository _addressRepo;
//     public TeacherRepository(CourseContext context, IMapper mapper, IAddressRepository addressRepo)
//     {
//       _addressRepo = addressRepo;
//       _mapper = mapper;
//       _context = context;
//     }

//     public Task AddCourseToStudentAsync(AddCourseToStudentViewModel studentCourse)
//     {
//       throw new NotImplementedException();
//     }

//     public Task AddStudentAsync(PostUserViewModel studentModel)
//     {
//       throw new NotImplementedException();
//     }

//     public async Task AddTeacherAsync(PostUserViewModel teachModel)
//     {
//       var addressToAdd = new Address();
//       addressToAdd.Street = teachModel.Street;
//       addressToAdd.Number = teachModel.Number;
//       addressToAdd.Zipcode = teachModel.Zipcode;
//       addressToAdd.City = teachModel.City;

//       var teacherToAdd = new User();
//       teacherToAdd.FirstName = teachModel.FirstName;
//       teacherToAdd.LastName = teachModel.LastName;
//       teacherToAdd.Email = teachModel.Email;
//       teacherToAdd.Phone = teachModel.Phone;



//       await _context.Addresses.AddAsync(addressToAdd);
//       await _addressRepo.SaveAllAsync();
//       teacherToAdd.AddressId = _context.Addresses.OrderBy(c => c.Id).Last().Id;

//       await _context.Users.AddAsync(teacherToAdd);
//     }

//     public void DeleteStudent(int id)
//     {
//       throw new NotImplementedException();
//     }

//     public void DeleteTeacher(int id)
//     {
//       var teacher = _context.Users.Find(id);

//       if (teacher is not null)
//       {
//         _addressRepo.DeleteAddress(teacher.AddressId);
//       }
//     }

//     public Task<List<UserViewModel>> GetAllStudentsAsync()
//     {
//       throw new NotImplementedException();
//     }

//     public async Task<bool> SaveAllAsync()
//     {
//       return await _context.SaveChangesAsync() > 0;
//     }
//   }
// }