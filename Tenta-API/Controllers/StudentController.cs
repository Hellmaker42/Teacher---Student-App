using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.Student;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/student")]
  public class StudentController : ControllerBase
  {
    private readonly IStudentRepository _studentRepo;
    public StudentController(IStudentRepository studentRepo)
    {
      _studentRepo = studentRepo;
    }

    [HttpPost()]
    public async Task<ActionResult> AddStudent(PostStudentViewModel studentModel)
    {
      await _studentRepo.AddStudentAsync(studentModel);

      if (await _studentRepo.SaveAllAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Det gick inte att spara eleven.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAddress(int id)
    {
      _studentRepo.DeleteStudent(id);

      if (await _studentRepo.SaveAllAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }
  }
}