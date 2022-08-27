using AutoMapper;
using Tenta_API.Model;
using Tenta_API.ViewModel;
using Tenta_API.ViewModel.Address;
using Tenta_API.ViewModel.Category;
using Tenta_API.ViewModel.Course;
using Tenta_API.ViewModel.Length;
using Tenta_API.ViewModel.User;

namespace Tenta_API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
      CreateMap<PostCourseViewModel, Course>();
      CreateMap<Course, CourseViewModel>()
        .ForMember(dest => dest.CourseId, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.CourseNumber, options => options.MapFrom(src => src.Number))
        .ForMember(dest => dest.CourseTitle, options => options.MapFrom(src => src.Title))
        .ForMember(dest => dest.CourseDescription, options => options.MapFrom(src => src.Description))
        .ForMember(dest => dest.CourseDetails, options => options.MapFrom(src => src.Details))
        .ForMember(dest => dest.CourseIsVideo, options => options.MapFrom(src => src.IsVideo))
        .ForMember(dest => dest.CourseCategoryId, options => options.MapFrom(src => src.CategoryId))
        .ForMember(dest => dest.CourseCategory, options => options.MapFrom(src => src.Category));
        
      CreateMap<PostCategoryViewModel, Category>();
      CreateMap<Category, CategoryViewModel>()
        .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Name));

      CreateMap<PostLengthViewModel, Length>();

      CreateMap<PostUserViewModel, User>();
      CreateMap<User, UserViewModel>()
        .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.UserFirstName, options => options.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.UserLastName, options => options.MapFrom(src => src.LastName))
        .ForMember(dest => dest.UserEmail, options => options.MapFrom(src => src.Email))
        .ForMember(dest => dest.UserPhone, options => options.MapFrom(src => src.Phone))
        .ForMember(dest => dest.UserStudentOrTeacher, options => options.MapFrom(src => src.StudentOrTeacher))
        .ForMember(dest => dest.UserAddress, options => options.MapFrom(src => src.Address))
        .ForMember(dest => dest.UserCourses, options => options.MapFrom(src => src.Course));

      CreateMap<PostAddressViewModel, Address>();
      CreateMap<Address, AddressViewModel>()
        .ForMember(dest => dest.AddressStreet, options => options.MapFrom(src => src.Street))
        .ForMember(dest => dest.AddressNumber, options => options.MapFrom(src => src.Number))
        .ForMember(dest => dest.AddressZipcode, options => options.MapFrom(src => src.Zipcode))
        .ForMember(dest => dest.AddressCity, options => options.MapFrom(src => src.City));

      // CreateMap<PostStudentViewModel, Student>()
      //   .ForMember(dest => dest.Address.Street, options => options.MapFrom(src => src.Street))
      //   .ForMember(dest => dest.Address.Number, options => options.MapFrom(src => src.Number))
      //   .ForMember(dest => dest.Address.Zipcode, options => options.MapFrom(src => src.Zipcode))
      //   .ForMember(dest => dest.Address.City, options => options.MapFrom(src => src.City));        
    }
  }
}