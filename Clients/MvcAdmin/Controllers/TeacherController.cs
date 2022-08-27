using Microsoft.AspNetCore.Mvc;
using MvcAdmin.Models;
using MvcAdmin.ViewModels;

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

    [HttpGet("CreateTeacher")]
    public IActionResult CreateTeacher()
    {
      var teacher = new CreateUserViewModel();
      return View("CreateTeacher", teacher);
    }

    [HttpPost("TeacherQual")]
    public async Task<IActionResult> TeacherQual(CreateUserViewModel teacher)
    {
      // Class.Session.Email = teacher.Email;
      // Class.Session.FirstName = teacher.FirstName;
      // Class.Session.LastName = teacher.LastName;

      var userModel = await _teacherService.GetTeacherByEmail(teacher.Email);

      Class.UserSession.User = userModel;

      var courses = await _teacherService.GetAllCourses();
      Class.CoursesSession.Courses = courses;

      return View("TeacherQual", courses);
    }

    [Route("AddQualToTeacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> AddQualToTeacher(int id)
    {
      var course = await _teacherService.GetCourseById(id);
      Class.UserSession.AddCourse(course);

      Class.CoursesSession.RemoveCourse(id);
      var courses = Class.CoursesSession.Courses;
      return View("TeacherQual", courses);
    }

    [Route("RemoveQualFromTeacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> RemoveQualFromTeacher(int id)
    {
      var course = await _teacherService.GetCourseById(id);
      Class.CoursesSession.AddCourse(course);

      Class.UserSession.RemoveCourse(id);
      var courses = Class.CoursesSession.Courses;
      return View("TeacherQual", courses);
    }

    [HttpGet("SaveQualToTeacher")]
    public async Task<IActionResult> SaveQualToTeacher()
    {
      // int id = Class.UserSession.User!.UserId;
      // var teacherModel = Class.UserSession.User;
      // teacherModel.UserCourses = Class.UserSession.Courses;
      // await _teacherService.UpdateTeacher(id, teacherModel);

      AddQualToTeacherViewModel qualModel = new();
      qualModel.TeacherEmail = Class.UserSession.User!.UserEmail;
      foreach (var course in Class.UserSession.Courses!)
      {
        qualModel.CourseIds!.Add(course.CourseId);
      }
      await _teacherService.UpdateQualToTeacher(qualModel);
      Class.UserSession.Courses!.Clear();
     
      return View("Confirmation");
    }

    //TODO:
    [HttpGet("EditQualToTeacher/{id}")]
    public async Task<IActionResult> EditQualToTeacher(int id)
    {
      var userModel = await _teacherService.GetTeacherById(id);
      var courses = await _teacherService.GetAllCourses();
      Class.CoursesSession.Courses = courses;

      return View("EditQualToTeacher", userModel);
    }

    [HttpPost("CreateTeacher")]
    public async Task<IActionResult> CreateTeacher(CreateUserViewModel teacher)
    {
      ViewBag.EmailError = null;
      teacher.StudentOrTeacher = true;
      if (await _teacherService.CheckEmail(teacher.Email))
      {
        ViewBag.EmailError = "Epostadressen du angivit finns redan i systemet.";
        return View("CreateTeacher", teacher);
      }

      if (!ModelState.IsValid)
      {
        return View("Error");
      }

      if (await _teacherService.CreateTeacher(teacher))
      {
        return await TeacherQual(teacher);
      }

      return View("CreateTeacher", teacher);
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

    [HttpGet("GetTeacherById/{id}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
      var teacher = await _teacherService.GetTeacherById(id);
      return Ok(teacher);
    }

    [HttpGet("EditTeacher/{id}")]
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

    // [Route("AddQualToTeacher")]
    // [HttpPut("{id}")]
    // public async Task<IActionResult> AddQualToTeacher(int id)
    // {
    //   AddQualToTeacherViewModel model = new AddQualToTeacherViewModel
    //   {
    //     CourseId = id,
    //     TeacherEmail = Class.UserSession.User!.UserEmail
    //   };
    //   await _teacherService.AddQualToTeacher(model);

    //   return View("Confirmation");
    // }  


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