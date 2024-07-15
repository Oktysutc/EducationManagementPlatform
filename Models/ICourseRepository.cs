using System.Collections.Generic;

namespace EducationManagementPlatform.Models
{
    public interface ICourseRepository : IRepository<Course>
    {
        void Update(Course course);
        void Save();
        IEnumerable<Course> SearchCourses(string query);
        Course Get(int id);
        IEnumerable<Course> GetCoursesByCategory(string categoryId);
    }
}
