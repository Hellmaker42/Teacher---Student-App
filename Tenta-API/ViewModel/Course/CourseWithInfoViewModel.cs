using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenta_API.ViewModel.Length;

namespace Tenta_API.ViewModel.Course
{
  public class CourseWithInfoViewModel
  {
    public int CourseId { get; set; }
    public int CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseDescription { get; set; }
    public string? CourseDetails { get; set; }
    public bool CourseIsVideo { get; set; }
    public int CourseCategoryId { get; set; }
    public int CourseLengthId { get; set; }
    public string? CourseVideoDescription { get; set; }
    // public string? CourseCategoryName { get; set; }
    public LengthViewModel CourseLength { get; set; } = new LengthViewModel();
  }
}