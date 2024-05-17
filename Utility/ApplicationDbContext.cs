using EducationManagementPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationManagementPlatform.Utility
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
       
        public DbSet<CourseCategory>CourseCategories { get; set; }
    }
}
