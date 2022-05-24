using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tenta_API.ViewModel
{
  public class CourseViewModel
  {
    public int CourseId { get; set; }
    public int CourseNumber { get; set; }
    public string? CourseTitle { get; set; }
    public int CourseLength { get; set; }
    public string? CourseInfo { get; set; }
  }
}