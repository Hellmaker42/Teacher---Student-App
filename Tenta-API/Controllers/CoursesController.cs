using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly CourseContext _context;
    private readonly ICourseRepository _courseRepo;
    private readonly IMapper _mapper;
    public CoursesController(CourseContext context, ICourseRepository courseRepo, IMapper mapper)
    {
      _mapper = mapper;
      _courseRepo = courseRepo;
      _context = context;
    }
    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> GetAllCourses()
    {
      var response = await _courseRepo.GetAllCoursesAsync();
      return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourse(int id)
    {
      try
      {
        var response = await _courseRepo.GetCourseAsync(id);
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

    [HttpGet("bycategory/{category}")]
    public async Task<List<CourseViewModel>> GetCourseByCategory(string category)
    {
      var response = await _courseRepo.GetCourseByCategoryAsync(category);
      return response;
    }

    [HttpPost()]
    public async Task<ActionResult<Course>> AddCourse(PostCourseViewModel course)
    {
      var courseToAdd = _mapper.Map<Course>(course);

      await _context.Courses.AddAsync(courseToAdd);
      await _context.SaveChangesAsync();
      return StatusCode(201, course);
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