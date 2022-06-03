using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Tenta_API.Data;
using Tenta_API.Interfaces;
using Tenta_API.Model;
using Tenta_API.ViewModel;
using Tenta_API.ViewModel.Category;
using Tenta_API.ViewModel.Course;

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

    public async Task AddCourseAsync(PostCourseViewModel model)
    {
      var courseToAdd = _mapper.Map<Course>(model);
      await _context.Courses.AddAsync(courseToAdd);
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

    public async Task<CourseViewModel?> GetCourseByNumberAsync(int number)
    {
      return await _context.Courses.Where(c => c.Number == number)
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<CourseWithCategoryViewModel> GetCourseWithCategoryAsync(int id)
    {
      var course = await _context.Courses.FindAsync(id);

      if (course is null)
      {
        throw new Exception($"Vi kunde inte hitta någon kurs med id: {id}");
      }

      var length = await _context.Lengths.FindAsync(course.LengthId);
      var category = await _context.Categories.FindAsync(course.CategoryId);
      var courseCat = new CourseWithCategoryViewModel
      {
        CourseId = course.Id,
        CourseNumber = course.Number,
        CourseTitle = course.Title,
        CourseDescription = course.Description,
        CourseDetails = course.Details,
        CourseIsVideo = course.IsVideo,
        CategoryName = category!.Name

      };
      courseCat.CourseVideoDescription = (course.IsVideo) ? $"Detta är en videokurs som är {length!.Hours} timmar och {length!.Minutes} minuter lång." : $"Detta är en vanlig kurs som är {length!.Days} dagar lång.";

      return courseCat;

    }

    public async Task<CourseWithInfoViewModel> GetCourseWithInfoAsync(int id)
    {
      var course = await _context.Courses.FindAsync(id);

      if (course is null)
      {
        throw new Exception($"Vi kunde inte hitta någon kurs med id: {id}");
      }

      var length = await _context.Lengths.FindAsync(course.LengthId);
      var category = await _context.Categories.FindAsync(course.CategoryId);
      var courseInfo = new CourseWithInfoViewModel
      {
        CourseId = course.Id,
        CourseNumber = course.Number,
        CourseTitle = course.Title,
        CourseDescription = course.Description,
        CourseDetails = course.Details,
        CourseIsVideo = course.IsVideo
        // CategoryName = category!.Name

      };
      courseInfo.CourseVideoDescription = (course.IsVideo) ? $"Detta är en videokurs som är {length!.Hours} timmar och {length!.Minutes} minuter lång." : $"Detta är en vanlig kurs som är {length!.Days} dagar lång.";

      return courseInfo;

    }

    public async Task<List<CourseWithInfoViewModel>> GetCategoryWithCoursesAndInfoAsync(int id)
    {

      return await _context.Courses.Where(c => c.CategoryId == id)
        .Select(cm => new CourseWithInfoViewModel{
          CourseId = cm.Id,
          CourseNumber = cm.Number,
          CourseTitle = cm.Title,
          CourseDescription = cm.Description,
          CourseDetails = cm.Description,
          CourseIsVideo = cm.IsVideo,
          CourseLengthId = cm.LengthId,
          CourseCategoryId = cm.CategoryId,
          CourseVideoDescription = (cm.IsVideo) ? $"Detta är en videokurs som är {cm.Length.Hours} timmar och {cm.Length.Minutes} minuter lång." : $"Detta är en vanlig kurs som är {cm.Length.Days} dagar lång."
        }).ToListAsync();

    }

    public async Task<List<CourseViewModel>> GetCoursesByCategoryAsync(int id)
    {
      return await _context.Courses.Where(c => c.CategoryId == id)
        .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();
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
    public void DeleteCourse(int id)
    {
      var response = _context.Courses.Find(id);
      if (response is not null)
      {
        _context.Courses.Remove(response);
      }
    }
    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}