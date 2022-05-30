using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenta_API.ViewModel.Length;

namespace Tenta_API.Interfaces
{
    public interface ILengthRepository
    {
        public Task AddLengthAsync(PostLengthViewModel model);
        public Task UpdateLengthAsync(PostLengthViewModel model, int id);
        public Task<bool> SaveAllChangesAsync();
    }
}