using AutoMapper;
using Tenta_API.Model;
using Tenta_API.ViewModel;

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
        .ForMember(dest => dest.CourseLength, options => options.MapFrom(src => src.Length))
        .ForMember(dest => dest.CourseCategory, options => options.MapFrom(src => src.Category))
        .ForMember(dest => dest.CourseDescription, options => options.MapFrom(src => src.Description))
        .ForMember(dest => dest.CourseDetails, options => options.MapFrom(src => src.Details));
    }
  }
}