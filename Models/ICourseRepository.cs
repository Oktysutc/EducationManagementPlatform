namespace EducationManagementPlatform.Models
{
    public interface ICourseRepository :IRepository<Course>
    {
        void Update(Course course);
        void Save();
    }
}
