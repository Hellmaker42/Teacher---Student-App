using Microsoft.AspNetCore.Mvc;
using Tenta_API.Interfaces;
using Tenta_API.ViewModel.Address;

namespace Tenta_API.Controllers
{
  [ApiController]
  [Route("api/v1/address")]
  public class AddressController : ControllerBase
  {
    private readonly IAddressRepository _addressRepo;
    public AddressController(IAddressRepository addressRepo)
    {
      _addressRepo = addressRepo;
    }

    [HttpPost()]
    public async Task<ActionResult> AddAddress(PostAddressViewModel addressModel)
    {
      await _addressRepo.AddAddressAsync(addressModel);
      if (await _addressRepo.SaveAllAsync())
      {
        return StatusCode(201);
      }

      return StatusCode(500, "Det gick inte att spara adressen.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAddress(int id)
    {
      _addressRepo.DeleteAddress(id);

      if (await _addressRepo.SaveAllAsync())
      {
        return NoContent();
      }
      return StatusCode(500, "Något gick fel.");
    }

  }
}