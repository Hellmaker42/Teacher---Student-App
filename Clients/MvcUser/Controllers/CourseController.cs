using Microsoft.AspNetCore.Mvc;
using MvcUser.Models;

namespace MvcUser.Controllers
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

    [HttpGet("CategoriesWithCourse")]
    public async Task<IActionResult> GetAllCategoriesWithCourse()
    {
      try
      {
        var categories = await _courseService.GetAllCategoriesWithCourse();
        return View("Index", categories);
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
    public async Task<IActionResult> Details(int id)
    {
      try
      {
        var course = await _courseService.GetCourseWithId(id);
        return View("Details", course);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }
  }
}