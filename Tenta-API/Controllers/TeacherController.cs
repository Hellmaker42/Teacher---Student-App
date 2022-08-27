using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/teacher")]
  public class TeacherController : ControllerBase
  {
    private readonly ITeacherRepository _teachRepo;
    public TeacherController(ITeacherRepository teachRepo)
    {
      _teachRepo = teachRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<UserViewModel>>> GetAllTeachers()
    {
      var response = await _teachRepo.GetAllTeachersAsync();
      return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<UserViewModel>>> GetTeacherById(int id)
    {
      var response = await _teachRepo.GetTeacherByIdAsync(id);
      return Ok(response);
    }

    [HttpGet("GetTeacherByEmail/{email}")]
    public async Task<ActionResult<UserViewModel>> GetTeacherByEmail(string email)
    {
      var response = await _teachRepo.GetTeacherByEmailAsync(email);
      return Ok(response);
    }

    [HttpGet("CheckEmail/{email}")]
    public async Task<bool> CheckEmail(string email)
    {
      if (await _teachRepo.CheckEmailAsync(email)) return true;
      return false;
    }

    [HttpGet("LastCreated")]
    public async Task<ActionResult<int>> GetLastCreatedTeacher()
    {
      var response = await _teachRepo.GetLastCreatedTeacherAsync();
      return Ok(response);
    }

    [HttpPost()]
    public async Task<ActionResult> AddTeacher(PostUserViewModel teachModel)
    {
      await _teachRepo.AddTeacherAsync(teachModel);

      if (await _teachRepo.SaveAllAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Det gick inte att spara läraren.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTeacher(int id)
    {
      await _teachRepo.DeleteTeacherAsync(id);

      if (await _teachRepo.SaveAllAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> UpdateTeacher(int id, PostUserViewModel teacherModel)
    {
      try
      {
        await _teachRepo.UpdateTeacherAsync(id, teacherModel);

        if (await _teachRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, "Ett fel inträffade när läraren skulle uppdateras");

      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("UpdateQualToTeacher")]
    public async Task<ActionResult> UpdateQualToTeacher(AddQualToTeacherViewModel qualModel)
    {
      try
      {
        await _teachRepo.UpdateQualToTeacherAsync(qualModel);
        if (await _teachRepo.SaveAllAsync())
        {
          return NoContent();
        }
        return StatusCode(500, "Ett fel inträffade när läraren skulle uppdateras");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}