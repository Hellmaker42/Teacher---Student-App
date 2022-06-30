using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MvcAdmin.ViewModels;

namespace MvcAdmin.Models
{
  public class StudentServiceModel
  {
    private readonly string _baseUrl;
    private readonly IConfiguration _config;
    private readonly JsonSerializerOptions _options;
    public StudentServiceModel(IConfiguration config)
    {
      _config = config;
      _baseUrl = $"{_config.GetValue<string>("baseUrl")}student";
      _options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = true
      };
    }

    public async Task<List<UserViewModel>> GetAllStudents()
    {
      var url = $"{_baseUrl}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var students = await response.Content.ReadFromJsonAsync<List<UserViewModel>>();

      return students ?? new List<UserViewModel>();
    }

    public async Task<UserViewModel> GetStudentById(int id)
    {
      var url = $"{_baseUrl}/{id}";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det här gick ju inte bra tyvärr..");
      }
      var student = await response.Content.ReadFromJsonAsync<UserViewModel>();

      return student ?? new UserViewModel();
    }

    public async Task<bool> DeleteStudent(int id)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/{id}";

      var response = await http.DeleteAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        string reason = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reason);
        return false;
      }

      return true;
    }

    public async Task<bool> UpdateStudent(int id, UserViewModel studentModel)
    {
      using var http = new HttpClient();
      var url = $"{_baseUrl}/update/{id}";

      var postStudentModel = new PostUserViewModel();
      postStudentModel.FirstName = studentModel.UserFirstName;
      postStudentModel.LastName = studentModel.UserLastName;
      postStudentModel.Email= studentModel.UserEmail;
      postStudentModel.Phone = studentModel.UserPhone;
      postStudentModel.StudentOrTeacher = studentModel.UserStudentOrTeacher;
      postStudentModel.Street = studentModel.UserAddress!.AddressStreet;
      postStudentModel.Number = studentModel.UserAddress!.AddressNumber;
      postStudentModel.Zipcode = studentModel.UserAddress!.AddressZipCode;
      postStudentModel.City = studentModel.UserAddress!.AddressCity;

      var response = await http.PutAsJsonAsync(url, postStudentModel);

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