using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel;

namespace Tenta_API.Repositories
{
  public class CourseRepository : ICourseRepository
  {
    private readonly CourseContext _context;
    private readonly IMapper _mapper;
    public CourseRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public Task AddCourseAsync(Course model)
    {
      throw new NotImplementedException();
    }

    public void DeleteCourse(int id)
    {
      var response = _context.Courses.Find(id);
      if (response is not null)
      {
        _context.Courses.Remove(response);
      }
    }

    public async Task<List<CourseViewModel>> GetAllCoursesAsync()
    {
      return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CourseViewModel?> GetCourseAsync(int id)
    {
      return await _context.Courses.Where(c => c.Id == id)
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<CourseViewModel?> GetCourseAsync(string title)
    {
      return await _context.Courses.Where(c => c.Title!.ToLower() == title.ToLower())
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<List<CourseViewModel>> GetCourseByCategoryAsync(string category)
    {
      return await _context.Courses.Where(c => c.Category!.ToLower() == category.ToLower())
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
    {
      var course = await _context.Courses.FindAsync(id);
      _mapper.Map<PostCourseViewModel, Course>(model, course!);

      if (course is null)
      {
        throw new Exception($"Vi kunde inte hitta någon bil med id: {id}");
      }
      
      _context.Update(course);
    }
  }
}