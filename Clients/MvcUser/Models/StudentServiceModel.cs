using System.Text.Json;
using MvcUser.Class;
using MvcUser.ViewModels;

namespace MvcUser.Models
{
  public class StudentServiceModel
  {
    private readonly string _baseUrl;
    private readonly JsonSerializerOptions _options;
    private readonly IConfiguration _config;
    public StudentServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}student";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }
    public async Task<bool> CheckEmail(string email)
    {
      var url = $"{_baseUrl}/CheckEmail/{email}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<List<UserViewModel>> GetAllStudentsAsync()
    {
      var url = $"{_baseUrl}";
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

    public async Task<UserViewModel> GetStudentByEmail()
    {
      string email = Session.Email!;
      var url = $"{_baseUrl}/GetStudentByEmail/{email}";
      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var student = await response.Content.ReadFromJsonAsync<UserViewModel>();
      // var result = await response.Content.ReadAsStringAsync();
      // var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(result, _options);

      return student ?? new UserViewModel();
    }

    public async Task<bool> CreateStudent(CreateUserViewModel student)
    {
      var url = $"{_baseUrl}";
      using var http = new HttpClient();
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
      var url = $"{_baseUrl}/AddCourseToStudent";
      var response = await http.PutAsJsonAsync(url, model);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> RemoveCourseFromStudent(RemoveCourseFromStudentViewModel model)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/RemoveCourseFromStudent";
      var response = await http.PutAsJsonAsync(url, model);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;    
    }

    // public async Task<UserViewModel> GetStudentCourses()
    // {
    //   string email = Class.Session.Email!;

    //   using var http = new HttpClient();
    //   var url = $"{_baseUrl}/GetStudentCourses/{email}";
    //   var response = await http.GetAsync(url);

    //   if (!response.IsSuccessStatusCode)
    //   {
    //     string reason = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine(reason);
    //     return false;
    //   }

    //   return true;
    // }


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
