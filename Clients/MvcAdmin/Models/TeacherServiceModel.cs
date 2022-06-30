using System.Text.Json;
using MvcAdmin.ViewModels;
using MvcUser.ViewModels;

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
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}teacher";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<UserViewModel>> GetAllTeachers()
    {
      var url = $"{_baseUrl}";

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
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var teacher = await response.Content.ReadFromJsonAsync<UserViewModel>();

      return teacher ?? new UserViewModel();
    }

    public async Task<bool> CreateTeacher(CreateUserViewModel teacherModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}";

      var response = await http.PostAsJsonAsync(url, teacherModel);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    // public async Task<UserViewModel> UpdateTeacher(PostUserViewModel teacherModel)
    // {
    //   using var http = new HttpClient();
    //   var url = $"{_baseUrl}";

    //   var response = await http.PostAsJsonAsync(url, teacherModel);

    //   if (!response.IsSuccessStatusCode)
    //   {
    //     throw new Exception("Det här gick ju inte bra tyvärr..");
    //   }
    //   var teacher = await response.Content.ReadFromJsonAsync<UserViewModel>();

    //   return teacher ?? new UserViewModel();
    // }
    public async Task<bool> UpdateTeacher(int id, UserViewModel teacherModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/update/{id}";

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


    public async Task<bool> DeleteTeacher(int id)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/{id}";

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