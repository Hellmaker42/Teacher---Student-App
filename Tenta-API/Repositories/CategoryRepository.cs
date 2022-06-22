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
  public class CategoryRepository : ICategoryRepository
  {
    private readonly IMapper _mapper;
    private readonly CourseContext _context;
    public CategoryRepository(CourseContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task AddCategoryAsync(PostCategoryViewModel model)
    {
      var categoryToAdd = _mapper.Map<Category>(model);
      await _context.Categories.AddAsync(categoryToAdd);
    }

    public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
    {
      return await _context.Categories.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
    {
      return _mapper.Map<CategoryViewModel>(await _context.Categories.FindAsync(id));
    }

    public async Task<List<CategoryWithCoursesViewModel>> GetCategoryWithCoursesAsync()
    {
      var courses = await _context.Categories.Include(co => co.Courses)
        .Select(ccvm => new CategoryWithCoursesViewModel
        {
          CategoryId = ccvm.Id,
          CategoryName = ccvm.Name,
          Courses = ccvm.Courses
            .Select(com => new CourseWithInfoViewModel
            {
              CourseId = com.Id,
              CourseNumber = com.Number,
              CourseTitle = com.Title,
              CourseDescription = com.Description,
              CourseDetails = com.Details,
              CourseCategoryId = com.CategoryId,
              CourseLengthId = com.LengthId,
              CourseIsVideo = com.IsVideo,
              CourseVideoDescription = (com.IsVideo) ? $"Detta är en videokurs som är {com.Length!.Hours} timmar och {com.Length.Minutes} minuter lång." : $"Detta är en vanlig kurs som är {com.Length!.Days} dagar lång."
            }).ToList()
        }).ToListAsync();


        return courses;
    }

    public async Task UpdateCategoryAsync(PostCategoryViewModel model, int id)
    {
      var response = await _context.Categories.FindAsync(id);
      _mapper.Map<PostCategoryViewModel, Category>(model, response!);

      if (response is null)
      {
        throw new Exception($"Vi kunde inte hitta någon kategori med id: {id}");
      }

      _context.Update(response);
    }

    public void DeleteCategoryAsync(int id)
    {
      var response = _context.Categories.Find(id);

      if(response is not null)
      {
        _context.Categories.Remove(response);
      }
    }

    public async Task<bool> SaveAllChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}