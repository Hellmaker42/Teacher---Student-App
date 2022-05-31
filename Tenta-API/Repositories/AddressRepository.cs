using AutoMapper;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.Address;

namespace Tenta_API.Repositories
{
  public class AddressRepository : IAddressRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    public AddressRepository(CourseContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task AddAddressAsync(PostAddressViewModel addressModel)
    {
      var addressToAdd = _mapper.Map<Address>(addressModel);
      await _context.Addresses.AddAsync(addressToAdd);
    }

    public void DeleteAddress(int id)
    {
      var response = _context.Addresses.Find(id);
      if (response is not null)
      {
        _context.Addresses.Remove(response);
      }
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}