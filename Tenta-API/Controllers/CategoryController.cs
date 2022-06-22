using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.Category;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/category")]
  public class CategoryController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _catRepo;
    public CategoryController(ICategoryRepository catRepo, IMapper mapper)
    {
      _catRepo = catRepo;
      _mapper = mapper;
    }

    [HttpPost()]
    public async Task<ActionResult> AddCategory(PostCategoryViewModel model)
    {
      await _catRepo.AddCategoryAsync(model);
      if (await _catRepo.SaveAllChangesAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Något gick fel när kategorin skulle sparas.");
    }

    [HttpGet()]
    public async Task<ActionResult<List<CategoryViewModel>>> GetAllCategories()
    {
      var response = await _catRepo.GetAllCategoriesAsync();
      return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryViewModel>> GetCategoryById(int id)
    {
      return Ok(await _catRepo.GetCategoryByIdAsync(id));
      // var response = await _catRepo.GetCategoryByIdAsync(id);
      // if(response.CategoryName == "C++"){

      // }
    }

    [HttpGet("withcourses")]
    public async Task<ActionResult<List<CategoryWithCoursesViewModel>>> GetCategoryWithCourses()
    {
      try
      {
        var response = await _catRepo.GetCategoryWithCoursesAsync();
        return Ok(response);
        
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(PostCategoryViewModel model, int id)
    {
      try
      {
        await _catRepo.UpdateCategoryAsync(model, id);
        if (await _catRepo.SaveAllChangesAsync())
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
    public async Task<ActionResult> DeleteCategory(int id)
    {
      _catRepo.DeleteCategoryAsync(id);

      if (await _catRepo.SaveAllChangesAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }
  }
}