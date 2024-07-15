using EducationManagementPlatform.Utility;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationManagementPlatform.Models
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CourseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // SaveChanges metodunu kullanarak değişiklikleri veritabanına kaydeden metod
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        // Veritabanında bir kursu güncellemek için Update metodunu kullanan metod
        public void Update(Course course)
        {
            _applicationDbContext.Update(course);
        }

        // Belirli bir sorguyu içeren kursları aramak için LINQ sorgusu kullanan metod
        public IEnumerable<Course> SearchCourses(string query)
        {
            return _applicationDbContext.Courses
                .Where(c => c.CourseName.Contains(query) || c.Information.Contains(query))
                .Include(c => c.CourseCategory)
                .ToList();
        }

        // Belirli bir ID'ye sahip kursu veritabanından getiren metod
        public Course Get(int id)
        {
            return _applicationDbContext.Courses.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Course> GetCoursesByCategory(string category)
        {
            return _applicationDbContext.Courses
                .Include(c => c.CourseCategory)
                .Where(c => c.CourseCategory.Name.ToLower() == category.ToLower())
                .ToList();
        }
    }
}
