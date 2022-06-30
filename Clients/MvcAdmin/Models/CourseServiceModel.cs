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

      using var http = new HttpClient();
      var url = $"{_baseUrl}course";

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
      using var http = new HttpClient();
      var url = $"{_baseUrl}course/update/{id}";

      var response = await http.PutAsJsonAsync(url, courseModel);

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
      var url = $"{_baseUrl}course/{id}";

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

    // public async Task<SelectCategoryViewModel> GetSelectCategoryAsync()
    // {
    //   var categories = GetAllCategories();

    //   var catList = (from product in categories
    //                       select new SelectListItem()
    //                       {
    //                         Text = product.Name,
    //                         Value = product.ProductId.ToString(),
    //                       }).ToList();

    //   productsList.Insert(0, new SelectListItem()
    //   {
    //     Text = "----Select----",
    //     Value = string.Empty
    //   });

    //   ProductViewModel productViewModel = new ProductViewModel();
    //   productViewModel.Listofproducts = productsList;
    // }
  }
}