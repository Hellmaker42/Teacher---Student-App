using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/user")]
  public class UserController : ControllerBase
  {
    private readonly IUserRepository _userRepo;
    private readonly IAddressRepository _addressRepo;
    public UserController(IUserRepository userRepo, IAddressRepository addressRepo)
    {
      _addressRepo = addressRepo;
      _userRepo = userRepo;
    }

    [HttpGet()]
    public async Task<ActionResult<List<UserViewModel>>> GetAllStudents()
    {
      var students = await _userRepo.GetAllStudentsAsync();
      return students;
    }

    [HttpPost()]
    public async Task<ActionResult> AddStudent(PostUserViewModel userModel)
    {
      await _userRepo.AddStudentAsync(userModel);

      // await _userRepo.SaveAllAsync();
      // await _addressRepo.SaveAllAsync();
      
        // await _addressRepo.SaveAllAsync();
        return StatusCode(201);
      

      // return StatusCode(500, "Det gick inte att spara eleven.");
    }

    // [HttpPost("addcoursetostudent")]
    // public async Task<ActionResult> AddCourseToStudentAsync(AddCourseToStudentViewModel studentCourse)
    // {
    //   await _userRepo.AddCourseToStudentAsync(studentCourse);

    //   if (await _userRepo.SaveAllAsync())
    //   {
    //     return StatusCode(201);
    //   }

    //   return StatusCode(500, "Det gick inte att spara eleven.");
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteAddress(int id)
    // {
    //   _userRepo.DeleteStudent(id);

    //   if (await _userRepo.SaveAllAsync())
    //   {
    //     return NoContent();
    //   }
    //   return StatusCode(500, "Något gick fel.");
    // }
  }
}