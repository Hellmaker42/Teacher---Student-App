using Microsoft.AspNetCore.Mvc;
using MvcAdmin.Models;
using MvcAdmin.ViewModels;
using MvcUser.ViewModels;

namespace MvcAdmin.Controllers
{
  [Route("[controller]")]
  public class TeacherController : Controller
  {
    private readonly IConfiguration _config;
    private readonly TeacherServiceModel _teacherService;
    public TeacherController(IConfiguration config)
    {
      _config = config;
      _teacherService = new TeacherServiceModel(_config);
    }

    public IActionResult Index()
    {
      return View();
    }

    // public IActionResult Error()
    // {
    //   return View();
    // }

    [HttpGet("CreateTeacher")]
    public IActionResult CreateTeacher()
    {
      var teacher = new CreateUserViewModel();
      return View("CreateTeacher", teacher);
      // return View("Error");
    }

    [HttpPost("CreateTeacher")]
    public async Task<IActionResult> CreateTeacher(CreateUserViewModel teacher)
    {
      var teacherModel = new CreateUserViewModel()
      {
        FirstName = teacher.FirstName,
        LastName = teacher.LastName,
        Email = teacher.Email,
        Phone = teacher.Phone,
        StudentOrTeacher = true,
        Street = teacher.Street,
        Number = teacher.Number,
        Zipcode = teacher.Zipcode,
        City = teacher.City
      };

      if (!ModelState.IsValid)
      {
        // return View("CreateStudent", student);
        return View("Error");
      }

      if (await _teacherService.CreateTeacher(teacherModel))
      {
        return View("Confirmation");
      }

      return View("CreateTeacher", teacher);
      // return View("Error");
    }

    [HttpGet("GetAllTeachers")]
    public async Task<IActionResult> GetAllTeachers()
    {
      try
      {
        var teachers = await _teacherService.GetAllTeachers();
        return View("ListAllTeachers", teachers);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(int id)   
    {
      var teacher = await _teacherService.GetTeacherById(id);
      return Ok(teacher);
    } 

    [Route("EditTeacher")]
    [HttpGet("{id}")]
    public async Task<IActionResult> EditTeacher(int id)
    {
      var teacher = await _teacherService.GetTeacherById(id);
      return View("EditTeacher", teacher);
    }

    [Route("UpdateTeacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, UserViewModel teacherModel)
    {
      var teacher = await _teacherService.UpdateTeacher(id, teacherModel);
      // return Ok(teacher);
      return View("Confirmation");
    }    

    [Route("DeleteTeacher")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
      try
      {
        await _teacherService.DeleteTeacher(id);

        var teachers = await _teacherService.GetAllTeachers();
        return View("ListAllTeachers", teachers);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }
  }
}