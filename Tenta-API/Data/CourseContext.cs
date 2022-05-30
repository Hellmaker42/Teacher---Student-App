using Microsoft.EntityFrameworkCore;
using Tenta_API.Model;

namespace Tenta_API.Data
{
  public class CourseContext : DbContext
  {

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Length> Lengths => Set<Length>();
    public CourseContext(DbContextOptions options) : base(options)
    {
    }
  }
}