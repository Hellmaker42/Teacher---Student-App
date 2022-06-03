using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MvcUser.ViewModels;

namespace MvcUser.Models
{
  public class CourseServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;

    public CourseServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<CourseViewModel>> GetAllCourses()
    {
      var url = $"{_baseUrl}courses";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var courses = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
      // var result = await response.Content.ReadAsStringAsync();
      // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

      return courses ?? new List<CourseViewModel>();
    }

    public async Task<CourseViewModel> GetCourseWithId(int id)
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

    public async Task<List<CourseViewModel>> GetCoursesByCategory(int id)
    {
      var url = $"{_baseUrl}bycategory/{id}";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var categories = await response.Content.ReadFromJsonAsync<List<CourseViewModel>>();
      return categories ?? new List<CourseViewModel>();
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

    public async Task<List<CategoryWithCoursesViewModel>> GetAllCategoriesWithCourses()
    {
      var url = $"{_baseUrl}withcategory";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }

      var courses = await response.Content.ReadFromJsonAsync<List<CategoryWithCoursesViewModel>>();
      return courses ?? new List<CategoryWithCoursesViewModel>();
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
  }
}