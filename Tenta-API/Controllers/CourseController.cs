using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.Course;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CourseController : ControllerBase
  {

    private readonly ICourseRepository _courseRepo;
    private readonly ICategoryRepository _catRepo;
    private readonly IMapper _mapper;
    public CourseController(ICourseRepository courseRepo, ICategoryRepository catRepo, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepo = courseRepo;
      _catRepo = catRepo;
    }
    [HttpPost()]
    public async Task<ActionResult> AddCourse(PostCourseViewModel model)
    {
      if (await _courseRepo.GetCourseByNumberAsync(model.Number) is not null)
      {
        return BadRequest($"Kursnummer {model.Number} finns redan i systemet.");
      }

      var category = await _catRepo.GetCategoryByNameAsync(model.Category!.Name!);

      if (category is not null)
      {
        model.Category = null;
        model.CategoryId = category.CategoryId;
      }


      await _courseRepo.AddCourseAsync(model);
      if (await _courseRepo.SaveAllAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Det gick inte att spara kursen.");
    }

    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> GetAllCourses()
    {
      var response = await _courseRepo.GetAllCoursesAsync();
      return Ok(response);
    }

    // [HttpGet("CoursesToTeacher")]
    // public async Task<ActionResult<List<CourseViewModel>>> GetCoursesToTeacher()
    // {
    //   var response = await _courseRepo.GetAllCoursesAsync();
    //   return Ok(response);
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseById(int id)
    {
      try
      {
        var response = await _courseRepo.GetCourseByIdAsync(id);
        if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id}.");

        return Ok(response);
      }

      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

    }

    [HttpGet("bytitle/{title}")]
    public async Task<ActionResult<CourseViewModel>> GetCourse(string title)
    {
      var response = await _courseRepo.GetCourseAsync(title);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med Titel: {title}.");

      return Ok(response);
    }

    // [HttpGet("bynumber/{number}")]
    // public async Task<ActionResult<CourseViewModel>> GetCourseByNumber(int number)
    // {
    //   var response = await _courseRepo.GetCourseByNumberAsync(number);
    //   if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med kursnummer: {number}.");

    //   return Ok(response);
    // }

    [HttpGet("bynumber/{number}")]
    public async Task<bool> CheckIfCourseNumberExists(int number)
    {
      var response = await _courseRepo.GetCourseByNumberAsync(number);
      if (response is null) return false;

      return true;
    }

    [HttpGet("bycategory/{id}")]
    public async Task<ActionResult<List<CourseViewModel>>> GetCoursesByCategory(int id)
    {
      try
      {
        var response = await _courseRepo.GetCoursesByCategoryAsync(id);
        if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med kategori-id: {id}.");

        return Ok(response);

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }

      // return StatusCode(201, "hej");
    }

    [HttpGet("withcategory/{id}")]
    public async Task<ActionResult<CourseWithCategoryViewModel>> GetCourseWithCategory(int id)
    {
      var response = await _courseRepo.GetCourseWithCategoryAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id}.");

      return Ok(response);
    }

    [HttpGet("withinfo/{id}")]
    public async Task<ActionResult<List<CourseWithInfoViewModel>>> GetCourseWithInfo(int id)
    {
      var response = await _courseRepo.GetCourseWithInfoAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id}.");

      return Ok(response);
    }

    [HttpGet("categorywithcourseandinfo/{id}")]
    public async Task<ActionResult<List<CourseWithInfoViewModel>>> GetCategorieWithCoursesAndInfo(int id)
    {
      var response = await _courseRepo.GetCategoryWithCoursesAndInfoAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kategori med id: {id}.");

      return Ok(response);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, PostCourseViewModel model)
    {
      try
      {
        await _courseRepo.UpdateCourseAsync(id, model);
        if (await _courseRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, "Ett fel inträffade när kursen skulle uppdateras");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
      _courseRepo.DeleteCourse(id);

      if (await _courseRepo.SaveAllAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }
  }
}