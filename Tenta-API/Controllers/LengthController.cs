using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.Length;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/length")]
  public class LengthController : ControllerBase
  {
    private readonly ILengthRepository _lengthRepo;
    public LengthController(ILengthRepository lengthRepo)
    {
      _lengthRepo = lengthRepo;
    }

    [HttpPost()]
    public async Task<ActionResult> AddLength(PostLengthViewModel model)
    {
      await _lengthRepo.AddLengthAsync(model);

      if(await _lengthRepo.SaveAllChangesAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Något gick fel...");      
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLength(PostLengthViewModel model, int id)
    {
      try
      {
        await _lengthRepo.UpdateLengthAsync(model, id);
        if (await _lengthRepo.SaveAllChangesAsync())
        {
          return NoContent();
        }
        return StatusCode(500, "Något gick fil vid uppdateringen...");
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLength(int id)
    {
      _lengthRepo.DeleteLengthAsync(id);

      if (await _lengthRepo.SaveAllChangesAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }
  }
}