using Microsoft.EntityFrameworkCore;
using Tenta_API.Model;

namespace Tenta_API.Data
{
  public class CourseContext : DbContext
  {

    public DbSet<Course> Courses => Set<Course>();

    public CourseContext(DbContextOptions options) : base(options)
    {
    }
  }
}