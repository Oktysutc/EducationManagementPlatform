namespace EducationManagementPlatform.Models
{
    public interface ICourseCategoryRepository :IRepository<CourseCategory>
    {
        void Update(CourseCategory courseCategory);
        void Save();
    }
}
