using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAdmin.ViewModels
{
    public class CourseViewModel
    {
    public int CourseId { get; set; }
    public int CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public string? CourseDescription { get; set; }
    public string? CourseDetails { get; set; }
    public bool CourseIsVideo { get; set; }
    public int CourseCategoryId { get; set; }
    public CreateCategoryViewModel? CourseCategory { get; set; }
    public int CourseLengthId { get; set; }
    public CreateLengthViewModel? CourseLength { get; set; }        
    }
}