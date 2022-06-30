using Microsoft.AspNetCore.Mvc;
using MvcUser.Models;
using MvcUser.ViewModels;

namespace MvcUser.Controllers
{

  [Route("[controller]")]
  public class StudentController : Controller
  {

    private readonly IConfiguration _config;
    private readonly StudentServiceModel _studentService;
    public StudentController(IConfiguration config)
    {
      _config = config;
      _studentService = new StudentServiceModel(_config);
    }

    [HttpGet("GetAllStudents")]
    public async Task<IActionResult> GetAllStudents()
    {
      try
      {
        var students = await _studentService.GetAllStudentsAsync();
        return View("Index", students);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }

    [HttpGet("CreateStudent")]
    public IActionResult Create()
    {
      var student = new CreateUserViewModel();
      return View("CreateStudent", student);
      // return View("Error");
    }

    [HttpPost("CreateStudent")]
    public async Task<IActionResult> Create(CreateUserViewModel student)
    {
      var studentModel = new CreateUserViewModel()
      {
        FirstName = student.FirstName,
        LastName = student.LastName,
        Email = student.Email,
        Phone = student.Phone,
        StudentOrTeacher = student.StudentOrTeacher,
        AddressId = 0,
        Street = student.Street,
        Number = student.Number,
        Zipcode = student.Zipcode,
        City = student.Zipcode
      };

      if (!ModelState.IsValid)
      {
        // return View("CreateStudent", student);
        return View("Error");
      }

      if (await _studentService.CreateStudent(studentModel))
      {
        Class.Session.FName = studentModel.FirstName;
        Class.Session.LName = studentModel.LastName;
        Class.Session.Email = studentModel.Email;

        return View("Confirmation");
      }

      return View("CreateStudent", student);
      // return View("Error");
    }

    [HttpPost("AddCorseToStudent/{id}")]
    public async Task<IActionResult> AddCourseToStudent(int id)
    {
      try
      {
        AddCourseToStudentViewModel model = new AddCourseToStudentViewModel
        {
          CourseId = id,
          StudentEmail = Class.Session.Email
        };

        await _studentService.AddCourseToStudent(model);
        return View("AddedCourse");

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }

    }
  }
}