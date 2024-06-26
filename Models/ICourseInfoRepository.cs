namespace EducationManagementPlatform.Models
{
    public interface ICourseInfoRepository :IRepository<CourseInfo>
    {//repositorylerin interfacesi burada kullnıldı
        void Update(CourseInfo courseInfo);
        void Save();
    }
}
