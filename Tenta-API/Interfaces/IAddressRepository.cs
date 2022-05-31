using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenta_API.ViewModel.Address;

namespace Tenta_API.Interfaces
{
  public interface IAddressRepository
  {
    public Task AddAddressAsync(PostAddressViewModel addressModel);
    public void DeleteAddress(int id);
    public Task<bool> SaveAllAsync();
  }
}