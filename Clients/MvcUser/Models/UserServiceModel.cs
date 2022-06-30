using System.Text.Json;
using MvcUser.ViewModels;

namespace MvcUser.Models
{
  public class UserServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;
    public UserServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<UserViewModel>> GetAllStudentsAsync()
    {
      var url = $"{_baseUrl}student";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var students = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
      // var result = await response.Content.ReadAsStringAsync();
      // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

      return students ?? new List<UserViewModel>();
    }

    public async Task<bool> CreateStudent(CreateUserViewModel student)
    {
      using var http = new HttpClient();
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}user";

      var response = await http.PostAsJsonAsync(url, student);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> AddCourseToStudent(AddCourseToStudentViewModel model)
    {
      using var http = new HttpClient();
      var baseUrl = _config.GetValue<string>("baseUrl");
      var url = $"{baseUrl}student/addcoursetostudent";


      var response = await http.PostAsJsonAsync(url, model);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    // public async Task<List<UserViewModel>> GetAllTeachersAsync()
    // {
    //   var url = $"{_baseUrl}teacher";

    //   using var http = new HttpClient();
    //   var response = await http.GetAsync(url);

    //   if (!response.IsSuccessStatusCode)
    //   {
    //     throw new Exception("Det här gick ju inte bra tyvärr..");
    //   }
    //   var students = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();
    //   // var result = await response.Content.ReadAsStringAsync();
    //   // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

    //   return students ?? new List<UserViewModel>();
    // }
  }
}
