// using Microsoft.AspNetCore.Mvc;
// using Tenta_API.Interfaces;
// using Tenta_API.ViewModel.User;

// namespace Tenta_API.Controllers
// {
  // [ApiController]
  // [Route("api/v1/user")]
  // public class UserController : ControllerBase
  // {
  //   private readonly IUserRepository _userRepo;
  //   private readonly IAddressRepository _addressRepo;
  //   public UserController(IUserRepository userRepo, IAddressRepository addressRepo)
  //   {
  //     _addressRepo = addressRepo;
  //     _userRepo = userRepo;
  //   }

    // [HttpGet("GettAllStudents")]
    // public async Task<ActionResult<List<UserViewModel>>> GetAllStudents()
    // {
    //   var students = await _userRepo.GetAllStudentsAsync();
    //   return students;
    // }

    // [HttpPost()]
    // public async Task<ActionResult> AddStudent(PostUserViewModel userModel)
    // {
    //   await _userRepo.AddStudentAsync(userModel);

    //   // await _userRepo.SaveAllAsync();
    //   // await _addressRepo.SaveAllAsync();
      
    //     // await _addressRepo.SaveAllAsync();
    //     return StatusCode(201);
      

      // return StatusCode(500, "Det gick inte att spara eleven.");
    // }

    // [HttpPost("addcoursetostudent")]
    // public async Task<ActionResult> AddCourseToStudentAsync(AddCourseToStudentViewModel studentCourse)
    // {
    //   await _userRepo.AddCourseToStudentAsync(studentCourse);

    //   if (await _userRepo.SaveAllAsync())
    //   {
    //     return StatusCode(201);
    //   }

    //   return StatusCode(500, "Det gick inte att spara eleven.");
    // }

//     [HttpGet("GetAllTeachers")]
//     public async Task<ActionResult<List<UserViewModel>>> GetAllTeachers()
//     {
//       var teachers = await _userRepo.GetAllTeachersAsync();
//       return teachers;
//     }
//   }
// }