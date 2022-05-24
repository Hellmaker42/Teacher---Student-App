using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Model;
using Tenta_API.ViewModel;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/courses")]
  public class CoursesController : ControllerBase
  {
    private readonly CourseContext _context;
    public CoursesController(CourseContext context)
    {
      _context = context;
    }
    [HttpGet()]
    public async Task<ActionResult<List<CourseViewModel>>> ListCourses()
    {
      var response = await _context.Courses.ToListAsync();
      var courseList = new List<CourseViewModel>();

      foreach (var course in response)
      {
        courseList.Add(
          new CourseViewModel {
            CourseId = course.Id,
            CourseNumber = course.Number,
            CourseTitle = course.Title,
            CourseLength = course.Length,
            CourseInfo = string.Concat("Beskrivning: ", course.Description, ", ", "Detaljer: ", course.Details)
          }
        );
      }

      return Ok(courseList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourseById(int id)
    {
      var response = await _context.Courses.FindAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id}.");
      return Ok(response);
    }

    [HttpGet("bytitle/{title}")]
    public async Task<ActionResult<Course>> GetCourseByTitle(string title)
    {
      var response = await _context.Courses.SingleOrDefaultAsync(c => c.Title!.ToLower() == title.ToLower());
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med Titel: {title}.");

      return Ok(response);
    }

    [HttpGet("bycategory/{category}")]
    public async Task<List<Course>> GetCourseByCategory(string category)
    {
      var response = await _context.Courses.Where(c => c.Category!.ToLower() == category.ToLower()).ToListAsync();
      return response;
    }

    [HttpPost()]
    public async Task<ActionResult<Course>> AddCourse(PostCourseViewModel course)
    {
      var courseToAdd = new Course{
        Title = course.Title,
        Number = course.Number,
        Length = course.Length,
        Category = course.Category,
        Description = course.Description,
        Details = course.Details
      };
      await _context.Courses.AddAsync(courseToAdd);
      await _context.SaveChangesAsync();
      return StatusCode(201, course);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(int id, Course model)
    {
      var response = await _context.Courses.FindAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id} som skulle uppdateras.");
      response.Number = model.Number;
      response.Title = model.Title;
      response.Length = model.Length;
      response.Category = model.Category;
      response.Description = model.Description;
      response.Details = model.Details;

      _context.Courses.Update(response);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(int id)
    {
      var response = await _context.Courses.FindAsync(id);
      if (response is null) return NotFound($"Vi kunde inte hitta någon kurs med id: {id} som skulle tas bort.");
      _context.Courses.Remove(response);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}