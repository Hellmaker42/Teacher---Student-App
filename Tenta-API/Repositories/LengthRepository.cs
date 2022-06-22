using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel.Length;

namespace Tenta_API.Repositories
{
  public class LengthRepository : ILengthRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    public LengthRepository(CourseContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task AddLengthAsync(PostLengthViewModel model)
    {
      var lengthToAdd = _mapper.Map<Length>(model);
      await _context.Lengths.AddAsync(lengthToAdd);
    }

    public async Task UpdateLengthAsync(PostLengthViewModel model, int id)
    {
      var length = await _context.Lengths.FindAsync(id);
      _mapper.Map<PostLengthViewModel, Length>(model, length!);

      if (length is null)
      {
        throw new Exception($"Vi kunde inte hitta någon längd med id: {id}");
      }
      _context.Update(length);
    }

    public void DeleteLengthAsync(int id)
    {
      var response = _context.Lengths.Find(id);

      if(response is not null)
      {
        _context.Lengths.Remove(response);
      }
    }

    public async Task<bool> SaveAllChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}