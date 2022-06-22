// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Tenta_API.Interfaces;
// using Tenta_API.ViewModel.Teacher;

// namespace Tenta_API.Controllers
// {
//   [ApiController]
//   [Route("api/v1/teacher")]
//   public class TeacherController : ControllerBase
//   {
//     private readonly ITeacherRepository _teachRepo;
//     public TeacherController(ITeacherRepository teachRepo)
//     {
//       _teachRepo = teachRepo;
//     }

//     [HttpPost()]
//     public async Task<ActionResult> AddStudent(PostTeacherViewModel teachModel)
//     {
//       await _teachRepo.AddTeacherAsync(teachModel);

//       if (await _teachRepo.SaveAllAsync())
//       {
//         return StatusCode(201);
//       }

//       return StatusCode(500, "Det gick inte att spara eleven.");
//     }

//     [HttpDelete("{id}")]
//     public async Task<ActionResult> DeleteTeacher(int id)
//     {
//       _teachRepo.DeleteTeacher(id);

//       if (await _teachRepo.SaveAllAsync())
//       {
//         return NoContent();
//       }
//       return StatusCode(500, "Något gick fel.");
//     }
//   }
// }