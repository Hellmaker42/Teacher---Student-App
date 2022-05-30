using AutoMapper;
using Tenta_API.Model;
using Tenta_API.ViewModel;
using Tenta_API.ViewModel.Category;
using Tenta_API.ViewModel.Course;
using Tenta_API.ViewModel.Length;

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
        .ForMember(dest => dest.CourseLengthId, options => options.MapFrom(src => src.LengthId));
        
      CreateMap<PostCategoryViewModel, Category>();
      CreateMap<Category, CategoryViewModel>()
        .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id))
        .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Name));

      CreateMap<PostLengthViewModel, Length>();
    }
  }
}