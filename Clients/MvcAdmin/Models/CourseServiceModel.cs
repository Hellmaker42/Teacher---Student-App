using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcAdmin.ViewModels;

namespace MvcAdmin.Models
{
  public class CourseServiceModel
  {
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    private readonly JsonSerializerOptions _options;
    public CourseServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<CourseWithInfoViewModel>> GetAllCourses()
    {
      var url = $"{_baseUrl}courses";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var courses = await response.Content.ReadFromJsonAsync<List<CourseWithInfoViewModel>>();

      return courses ?? new List<CourseWithInfoViewModel>();
    }

    public async Task<CourseViewModel> GetCourseById(int id)
    {
      var url = $"{_baseUrl}courses/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var course = await response.Content.ReadFromJsonAsync<CourseViewModel>();

      return course ?? new CourseViewModel();
    }

    public async Task<List<CategoryViewModel>> GetAllCategories()
    {
      var url = $"{_baseUrl}category";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
      return categories ?? new List<CategoryViewModel>();
    }

    public async Task<CategoryViewModel> GetCategoryById(int id)
    {
      var url = $"{_baseUrl}category/{id}";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var category = await response.Content.ReadFromJsonAsync<CategoryViewModel>();
      return category ?? new CategoryViewModel();
    }

    public async Task<List<CategoryViewModel>> GetAllCategoriesWithCourse()
    {
      var url = $"{_baseUrl}category/CatsWithCourse";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var categories = await response.Content.ReadFromJsonAsync<List<CategoryViewModel>>();
      return categories ?? new List<CategoryViewModel>();
    }

    public async Task<bool> CreateCategory(CreateCategoryViewModel categoryModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}category";

      var response = await http.PostAsJsonAsync(url, categoryModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<List<CourseWithInfoViewModel>> GetCategorieWithCoursesAndInfo(int id)
    {
      var url = $"{_baseUrl}courses/categorywithcourseandinfo/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var courses = await response.Content.ReadFromJsonAsync<List<CourseWithInfoViewModel>>();
      return courses ?? new List<CourseWithInfoViewModel>();
    }

    public async Task<bool> CreateCourse(CreateCourseViewModel courseModel)
    {
      courseModel.CategoryId = Int32.Parse(courseModel.Category!.Name!);

      var category = await GetCategoryById(courseModel.CategoryId);

      courseModel.Category.Id = category.CategoryId;
      courseModel.Category.Name = category.CategoryName;

      if (courseModel.Length!.Days is null) courseModel.Length.Days = 0;
      if (courseModel.Length!.Hours is null) courseModel.Length.Hours = 0;
      if (courseModel.Length!.Minutes is null) courseModel.Length.Minutes = 0;

      if (courseModel.IsVideo)
      {
        courseModel.Length!.Days = 0;
      }
      else if (!courseModel.IsVideo)
      {
        courseModel.Length!.Hours = 0;
        courseModel.Length!.Minutes = 0;
      }

      using var http = new HttpClient();
      var url = $"{_baseUrl}courses";

      var response = await http.PostAsJsonAsync(url, courseModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateCourse(int id, CourseViewModel courseModel)
    {
      courseModel.CourseCategoryId = Int32.Parse(courseModel.CourseCategory!.CategoryName!);

      var category = await GetCategoryById(courseModel.CourseCategoryId);

      courseModel.CourseCategory.CategoryId = category.CategoryId;
      courseModel.CourseCategory.CategoryName = category.CategoryName;

      // if (courseModel!.CourseLength!.LengthDays is null) courseModel.CourseLength.LengthDays = 0;
      // if (courseModel.CourseLength!.LengthHours is null) courseModel.CourseLength.LengthHours = 0;
      // if (courseModel.CourseLength!.LengthMinutes is null) courseModel.CourseLength.LengthMinutes = 0;

      if (courseModel.CourseIsVideo)
      {
        courseModel.CourseLength!.LengthDays = 0;
      }
      else if (!courseModel.CourseIsVideo)
      {
        courseModel.CourseLength!.LengthHours = 0;
        courseModel.CourseLength!.LengthMinutes = 0;
      }

      var course = new PostCourseViewModel
      {
        Number = courseModel.CourseNumber,
        Title = courseModel.CourseTitle,
        Description = courseModel.CourseDescription,
        Details = courseModel.CourseDetails,
        IsVideo = courseModel.CourseIsVideo,
        CategoryId = courseModel.CourseCategoryId,
        Category = new PostCategoryViewModel
        {
          Name = courseModel.CourseCategory!.CategoryName
        },
        Length = new PostLengthViewModel
        {
          Days = courseModel.CourseLength!.LengthDays,
          Hours = courseModel.CourseLength!.LengthHours,
          Minutes = courseModel.CourseLength!.LengthMinutes
        }
      };

      using var http = new HttpClient();
      var url = $"{_baseUrl}courses/{id}";

      var response = await http.PutAsJsonAsync(url, course);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> DeleteCourse(int id)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}courses/{id}";

      // var response = await http.PostAsJsonAsync(url, id);
      var response = await http.DeleteAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public bool CheckCourseNrOfDigits(int courseNr)
    {
      if(Enumerable.Range(1000, 9999).Contains(courseNr)) return true;
      return false;
    }

    public async Task<bool> CheckIfCourseNumberExists(int courseNr)
    {

      var url = $"{_baseUrl}courses/bynumber/{courseNr}";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      return await response.Content.ReadFromJsonAsync<bool>();
    }
  }
}