using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcAdmin.Models;
using MvcAdmin.ViewModels;

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
      try
      {
        if (!ModelState.IsValid)
        {
          return View("Error");
        }

        if (await _courseService.CreateCourse(courseModel))
        {
          return View("Confirmation");
        }
        return View("Error");
        // return View("CreateCourse", courseModel);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }

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

    [Route("CreateCategoryFromEdit")]
    [HttpPost("{info}")]
    public async Task<IActionResult> CreateCategoryFromEdit(string info)
    {
      int breakPointOne = info.IndexOf("?");
      string catName = info.Substring(0, breakPointOne);
      int breakPointTwo = info.IndexOf("=");
      string strId = info.Substring(breakPointTwo+1);
      int id = Int32.Parse(strId);
      

      var categoryModel = new CreateCategoryViewModel();
      categoryModel.Name = catName;

      if (!ModelState.IsValid)
      {
        return View("Error");
      }
      if (!await _courseService.CreateCategory(categoryModel))
      {
        return View("Error");
      }
      return await EditCourse(id);
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

    [HttpGet("CategoriesWithCourse")]
    public async Task<IActionResult> GetAllCategoriesWithCourse()
    {
      try
      {
        // var courseService = new CourseServiceModel(_config);

        var categories = await _courseService.GetAllCategoriesWithCourse();
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
      return View("EditCourse", course);
    }

    [Route("UpdateCourse")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, CourseViewModel courseModel)
    {
      try
      {
        if(await _courseService.UpdateCourse(id, courseModel))
        {
          return View("Confirmation");
        }
       return View("Error");        
      }
      catch (Exception ex)
      {
       return View("Error", ex);
      }
    }

    [Route("DeleteCourse")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
      try
      {
        await _courseService.DeleteCourse(id);

        var categories = await _courseService.GetAllCategoriesWithCourse();
        return View("Categories", categories);

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }


  }
}