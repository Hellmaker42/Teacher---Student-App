using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tenta_API.ViewModel.User
{
    public class AddCourseToStudentViewModel
    {
        public int CourseId { get; set; }
        public string? StudentEmail { get; set; }
    }
}