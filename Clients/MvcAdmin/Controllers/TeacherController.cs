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
      var userModel = await _teacherService.GetTeacherByEmail(teacher.Email);
      var coursesModel = await _teacherService.GetAllCourses();

      Class.UserSession.User = userModel;
      Class.CoursesSession.Courses = coursesModel;

      return View("TeacherQual", coursesModel);
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

    [Route("AddQualToTeacherFromEdit")]
    [HttpPut("{id}")]
    public async Task<IActionResult> AddQualToTeacherFromEdit(int id)
    {
      var course = await _teacherService.GetCourseById(id);
      Class.UserSession.AddCourseFromEdit(course);

      Class.CoursesSession.RemoveCourse(id);
      var courses = Class.CoursesSession.Courses;
      return View("EditQualToTeacher", courses);
    }

    [Route("RemoveQualFromTeacherFromEdit")]
    [HttpPut("{id}")]
    public async Task<IActionResult> RemoveQualFromTeacherFromEdit(int id)
    {
      var course = await _teacherService.GetCourseById(id);
      Class.CoursesSession.AddCourse(course);

      Class.UserSession.RemoveCourseFromEdit(id);
      var courses = Class.CoursesSession.Courses;
      return View("EditQualToTeacher", courses);
    }

    [HttpGet("SaveQualToTeacher")]
    public async Task<IActionResult> SaveQualToTeacher()
    {
      AddQualToTeacherViewModel qualModel = new();
      qualModel.TeacherEmail = Class.UserSession.User!.UserEmail;
      foreach (var course in Class.UserSession.Courses!)
      {
        qualModel.CourseIds!.Add(course.CourseId);
      }
      await _teacherService.UpdateQualToTeacher(qualModel);
      Class.UserSession.Courses!.Clear();
     
      var teachers = await _teacherService.GetAllTeachers();
      return View("ListAllTeachers", teachers);
    }


    [HttpGet("SaveQualToTeacherFromEdit")]
    public async Task<IActionResult> SaveQualToTeacherFromEdit()
    {
      AddQualToTeacherViewModel qualModel = new();
      qualModel.TeacherEmail = Class.UserSession.User!.UserEmail;
      foreach (var course in Class.UserSession.User!.UserCourses!)
      {
        qualModel.CourseIds!.Add(course.CourseId);
      }
      await _teacherService.UpdateQualToTeacherFromEdit(qualModel);
      Class.UserSession.User.UserCourses.Clear();
      Class.UserSession.Courses!.Clear();
     
     if(Class.UserSession.User.UserStudentOrTeacher)
     {
      var teachers = await _teacherService.GetAllTeachers();
      return View("ListAllTeachers", teachers);
     }
     else
     {
      return this.RedirectToAction("GetAllStudents", "Student");
     }

    }

    //TODO:
    [HttpGet("EditQualToTeacher/{id}")]
    public async Task<IActionResult> EditQualToTeacher(int id)
    {
      var userModel = await _teacherService.GetTeacherById(id);
      var coursesModel = await _teacherService.GetAllCourses();
      List<CourseViewModel> inputCoursesModel = new();
      bool userCourseExists = false;
      
      foreach (var course in coursesModel)
      {
        foreach (var userCourse in userModel.UserCourses!)
        {
          if(course.CourseId == userCourse.CourseId)
          {
            userCourseExists = true;
          }
        }
        if(!userCourseExists)
        {
          inputCoursesModel.Add(course);
        }
        userCourseExists = false;
      }

      Class.UserSession.User = userModel;
      Class.CoursesSession.Courses = inputCoursesModel;

      return View("EditQualToTeacher", inputCoursesModel);
    }

    // [Route("AddSingleQualToTeacher")]
    // [HttpPut("{id}")]
    // public async Task<IActionResult> AddSingleQualToTeacher(int id)
    // {
    //   AddSingleQualToTeacherViewModel singleQualModel = new();
    //   singleQualModel.CourseId = id;
    //   singleQualModel.TeacherEmail = Class.UserSession.User!.UserEmail;

    //   var course = await _teacherService.GetCourseById(id);
    //   Class.UserSession.AddCourse(course);

    //   // Class.CoursesSession.RemoveCourse(id);<
    //   var coursesModel = await _teacherService.GetAllCourses();
    //   return View("EditQualToTeacher", coursesModel);
    // }

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
      Class.UserSession.User = teacher;
      return View("EditTeacher", teacher);
    }

    [Route("UpdateTeacher")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, UserViewModel teacherModel)
    {
      if(Class.UserSession.User!.UserEmail! != teacherModel.UserEmail)
      {
        if (await _teacherService.CheckEmail(teacherModel.UserEmail!))
        {
          ViewBag.EmailError = "Epostadressen du angivit finns redan i systemet.";
          teacherModel = await _teacherService.GetTeacherById(id);
          return View("EditTeacher", teacherModel);
        }
      }
      var teacher = await _teacherService.UpdateTeacher(id, teacherModel);
      // return Ok(teacher);
        var teachers = await _teacherService.GetAllTeachers();
        return View("ListAllTeachers", teachers);
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