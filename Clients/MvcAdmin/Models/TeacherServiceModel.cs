using System.Text.Json;
using MvcAdmin.ViewModels;

namespace MvcAdmin.Models
{
  public class TeacherServiceModel
  {
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    private readonly JsonSerializerOptions _options;
    public TeacherServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<UserViewModel>> GetAllTeachers()
    {
      var url = $"{_baseUrl}teacher";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var teachers = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();

      return teachers ?? new List<UserViewModel>();
    }

    public async Task<UserViewModel> GetTeacherById(int id)
    {
      var url = $"{_baseUrl}teacher/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var teacher = await response.Content.ReadFromJsonAsync<UserViewModel>();

      return teacher ?? new UserViewModel();
    }

    public async Task<UserViewModel> GetTeacherByEmail(string email)
    {
      var url = $"{_baseUrl}teacher/GetTeacherByEmail/{email}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var teacher = await response.Content.ReadFromJsonAsync<UserViewModel>();

      return teacher ?? new UserViewModel();
    }

    public async Task<bool> CheckEmail(string email)
    {
      var url = $"{_baseUrl}teacher/CheckEmail/{email}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      return await response.Content.ReadFromJsonAsync<bool>();
    }

    public async Task<int> GetLastCreatedTeacher()
    {
      var url = $"{_baseUrl}teacher/LastCreated";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var teacherId = await response.Content.ReadFromJsonAsync<int>();

      return teacherId;
    }

    public async Task<bool> CreateTeacher(CreateUserViewModel teacherModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher";

      var response = await http.PostAsJsonAsync(url, teacherModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateTeacher(int id, UserViewModel teacherModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher/update/{id}";

      var postTeacherModel = new PostUserViewModel();
      postTeacherModel.FirstName = teacherModel.UserFirstName;
      postTeacherModel.LastName = teacherModel.UserLastName;
      postTeacherModel.Email= teacherModel.UserEmail;
      postTeacherModel.Phone = teacherModel.UserPhone;
      postTeacherModel.StudentOrTeacher = teacherModel.UserStudentOrTeacher;
      postTeacherModel.Street = teacherModel.UserAddress!.AddressStreet;
      postTeacherModel.Number = teacherModel.UserAddress!.AddressNumber;
      postTeacherModel.Zipcode = teacherModel.UserAddress!.AddressZipCode;
      postTeacherModel.City = teacherModel.UserAddress!.AddressCity;

      var response = await http.PutAsJsonAsync(url, postTeacherModel);

      

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }
      return true;
    }

    public async Task<bool> UpdateQualToTeacher(AddQualToTeacherViewModel qualModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher/UpdateQualToTeacher";      
      var response = await http.PutAsJsonAsync(url, qualModel);
  
      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateQualToTeacherFromEdit(AddQualToTeacherViewModel qualModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher/UpdateQualToTeacherFromEdit";      
      var response = await http.PutAsJsonAsync(url, qualModel);
  
      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> AddSingleQualToTeacher(AddSingleQualToTeacherViewModel singleQualModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher/AddSingleQualToTeacher";      
      var response = await http.PutAsJsonAsync(url, singleQualModel);
  
      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
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

      return courses ?? new List<CourseViewModel>();
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

    public async Task<bool> DeleteTeacher(int id)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}teacher/{id}";

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

  }
}