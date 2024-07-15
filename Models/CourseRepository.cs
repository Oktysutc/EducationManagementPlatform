using EducationManagementPlatform.Utility;
using Microsoft.EntityFrameworkCore;

namespace EducationManagementPlatform.Models
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public CourseRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
//repository design pattern burada kullanoıldı
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(Course course)
        {
            _applicationDbContext.Update(course);
        }
        public IEnumerable<Course> SearchCourses(string query)
        {
            return _applicationDbContext.Courses
                .Where(c => c.CourseName.Contains(query) || c.Information.Contains(query))
                .Include(c => c.CourseCategory)
                .ToList();
        }
        public Course Get(int id)
        {
            return _applicationDbContext.Courses.FirstOrDefault(c => c.Id == id);
        }
    }
}
