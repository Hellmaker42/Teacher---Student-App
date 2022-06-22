using Microsoft.EntityFrameworkCore;
using Tenta_API.Model;

namespace Tenta_API.Data
{
  public class CourseContext : DbContext
  {
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Length> Lengths => Set<Length>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Qualification> Qualifications => Set<Qualification>();
    // public DbSet<CourseStudent> CourseStudent => Set<CourseStudent>();
    public CourseContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // modelBuilder.Entity<CourseStudent>().HasKey(sc => new { sc.UserId, sc.CourseId });
      modelBuilder.Entity<Course>().HasOne(c => c.Category).WithMany(c => c.Courses);
      modelBuilder.Entity<Length>().HasMany(c => c.Courses).WithOne(l => l.Length);
      modelBuilder.Entity<User>().HasOne(a => a.Address).WithOne(u => u.User).HasForeignKey<Address>(a => a.UserId);

    }
  }
}