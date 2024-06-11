namespace EducationManagementPlatform.Models
{
    public interface ICourseCategoryRepository :IRepository<CourseCategory>
    {//repositorylerin interfacesi burada kullanıldı
        void Update(CourseCategory courseCategory);
        void Save();
    }
}
