using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcAdmin.Models;
using MvcAdmin.ViewModels;
using MvcUser.ViewModels;

namespace MvcAdmin.Controllers
{
  [Route("[controller]")]
  public class CourseController : Controller
  {
    private readonly IConfiguration _config;
    private readonly CourseServiceModel _courseService;
    public CourseController(IConfiguration config)
    {
      _config = config;
      _courseService = new CourseServiceModel(_config);
    }

    public IActionResult Index()
    {
      return View();
    }

    // public IActionResult Error()
    // {
    //   return View();
    // }

    [HttpGet("CreateCourse")]
    public async Task<IActionResult> CreateCourse()
    {
      var course = new CreateCourseViewModel();
      var categories = await _courseService.GetAllCategories();
      List<SelectListItem> catList = categories.ConvertAll(a =>
      {
        return new SelectListItem()
        {
          Text = a.CategoryName,
          Value = a.CategoryId.ToString()
        };
      });
      ViewBag.Categories = catList;

      return View("CreateCourse", course);
      // return View("Error");
    }

    [HttpPost("CreateCourse")]
    public async Task<IActionResult> CreateCourse(CreateCourseViewModel courseModel)
    {

      if (!ModelState.IsValid)
      {
        // return View("CreateStudent", student);
        return View("Error");
      }

      if (await _courseService.CreateCourse(courseModel))
      {
        return View("Confirmation");
      }

      return View("CreateCourse", courseModel);
      // return View("Error");
    }
    [Route("CreateCategory")]
    [HttpPost("{catName}")]
    public async Task<IActionResult> CreateCategory(string catName)
    {
      var categoryModel = new CreateCategoryViewModel();
      categoryModel.Name = catName;

      if (!ModelState.IsValid)
      {
        // return View("CreateStudent", student);
        return View("Error");
      }

      if (await _courseService.CreateCategory(categoryModel))
      {
        await CreateCourse();
      }

      return View("CreateCourse");
      // return View("Error");
    }

    [HttpGet("GetAllCourses")]
    public async Task<IActionResult> GetAllCourses()
    {
      try
      {
        var courses = await _courseService.GetAllCourses();
        return View("ListAllCourses", courses);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpGet("Category")]
    public async Task<IActionResult> GetAllCategories()
    {
      try
      {
        // var courseService = new CourseServiceModel(_config);

        var categories = await _courseService.GetAllCategories();
        return View("Categories", categories);
      }
      catch (System.Exception)
      {

        throw;
      }
    }

    [HttpGet("coursesbycategory/{id}")]
    public async Task<IActionResult> CoursesByCategory(int id)
    {
      try
      {
        var courses = await _courseService.GetCategorieWithCoursesAndInfo(id);
        return View("CoursesByCategory", courses);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> GetCourseDetails(int id)
    {
      var course = await _courseService.GetCourseById(id);
      return View("Details", course);
    }

    [Route("EditCourse")]
    [HttpGet("{id}")]
    public async Task<IActionResult> EditCourse(int id)
    {
      var course = await _courseService.GetCourseById(id);
      return View("EditCourse", course);
    }

    [Route("UpdateCourse")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, CourseViewModel courseModel)
    {
      var course = await _courseService.UpdateCourse(id, courseModel);
      // return Ok(course);
      return View("Confirmation");
    }

    [Route("DeleteCourse")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
      try
      {
        await _courseService.DeleteCourse(id);

        var courses = await _courseService.GetAllCourses();
        return View("ListAllCourses", courses);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }


  }
}