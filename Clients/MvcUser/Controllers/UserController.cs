// using Microsoft.AspNetCore.Mvc;
// using MvcUser.Models;
// using MvcUser.ViewModels;

// namespace MvcUser.Controllers
// {

//   [Route("[controller]")]
//   public class UserController : Controller
//   {

//     private readonly IConfiguration _config;
//     private readonly UserServiceModel _userService;
//     public UserController(IConfiguration config)
//     {
//       _config = config;
//       _userService = new UserServiceModel(_config);
//     }

//     [HttpGet("GetAllStudents")]
//     public async Task<IActionResult> GetAllStudents()
//     {
//       try
//       {
//         var students = await _userService.GetAllStudentsAsync();
//         return View("Index", students);
//       }
//       catch (Exception ex)
//       {
//         Console.WriteLine(ex.Message);
//         return View("Error");
//       }
//     }

//     [HttpGet("CreateStudent")]
//     public IActionResult Create()
//     {
//       var student = new CreateUserViewModel();
//       return View("CreateStudent", student);
//       // return View("Error");
//     }

//     [HttpPost("CreateStudent")]
//     public async Task<IActionResult> Create(CreateUserViewModel student)
//     {
//       var studentModel = new CreateUserViewModel(){
//         FirstName = student.FirstName,
//         LastName = student.LastName,
//         Email = student.Email,
//         Phone = student.Phone,
//         StudentOrTeacher = student.StudentOrTeacher,
//         AddressId = 0,
//         Street = student.Street,
//         Number = student.Number,
//         Zipcode = student.Zipcode,
//         City = student.Zipcode
//       };

//       if (!ModelState.IsValid)
//       {
//         // return View("CreateStudent", student);
//         return View("Error");
//       }

//       if (await _userService.CreateStudent(studentModel))
//       {
//         Class.Session.FName = studentModel.FirstName;
//         Class.Session.LName = studentModel.LastName;
//         Class.Session.Email = studentModel.Email;

//         return View("Confirmation");
//       }

//       return View("CreateStudent", student);
//       // return View("Error");
//     }

//     // [HttpGet("addcorsetostudent/{id}")]
//     // public IActionResult AddCourseToStudent(int id)
//     // {
//     //   var model = new AddCourseToStudentViewModel{
//     //     CourseId = id,
//     //     StudentEmail = Class.Session.Email
//     //   };
//     //   return View("addcorsetostudent", model);
//     // }

//     [HttpPost("AddCorseToStudent/{id}")]
//     public async Task<IActionResult> AddCourseToStudent(int id)
//     {
//       try
//       {
//         AddCourseToStudentViewModel model = new AddCourseToStudentViewModel{
//           CourseId = id,
//           StudentEmail = Class.Session.Email
//         };

//         await _userService.AddCourseToStudent(model);
//         return View("AddedCourse");
        
//       }
//       catch (Exception ex)
//       {
//         Console.WriteLine(ex.Message);
//         return View("Error");
//       }

//     }
//     [HttpGet("GetAllTeachers")]
//     public async Task<IActionResult> GetAllTeachers()
//     {
//       try
//       {
//         var teachers = await _userService.GetAllTeachersAsync();
//         return View("Index", teachers);
//       }
//       catch (Exception ex)
//       {
//         Console.WriteLine(ex.Message);
//         return View("Error");
//       }
//     }
//   }
// }