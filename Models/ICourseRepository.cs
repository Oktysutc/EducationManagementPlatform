namespace EducationManagementPlatform.Models
{
    public interface ICourseRepository :IRepository<Course>
    {//repositorylerin interfacesi burada kullnıldı
        void Update(Course course);
        void Save();
        IEnumerable<Course> SearchCourses(string query);
        Course Get(int id);
    }
}
