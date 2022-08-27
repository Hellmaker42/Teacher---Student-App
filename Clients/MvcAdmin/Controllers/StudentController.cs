using Microsoft.AspNetCore.Mvc;
using MvcAdmin.Models;
using MvcAdmin.ViewModels;

namespace MvcAdmin.Controllers
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

    public IActionResult Index()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }

    [HttpGet("GetAllStudents")]
    public async Task<IActionResult> GetAllStudents()
    {
      try
      {
        var students = await _studentService.GetAllStudents();
        return View("ListAllStudents", students);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)   
    {
      var student = await _studentService.GetStudentById(id);
      return Ok(student);
    } 

    [Route("DeleteStudent")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
      try
      {
        await _studentService.DeleteStudent(id);

        var students = await _studentService.GetAllStudents();
        return View("ListAllStudents", students);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }

    [Route("EditStudent")]
    [HttpGet("{id}")]
    public async Task<IActionResult> EditStudent(int id)
    {
      var student = await _studentService.GetStudentById(id);
      return View("EditStudent", student);
    }

    [Route("UpdateStudent")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, UserViewModel studentModel)
    {
      var student = await _studentService.UpdateStudent(id, studentModel);
      // return Ok(student);
      return View("Confirmation");
    }
  }
}