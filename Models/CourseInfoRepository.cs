using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class CourseInfoRepository : Repository<CourseInfo>, ICourseInfoRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public CourseInfoRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
            //repository design pattern burada kullanoıldı
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(CourseInfo courseInfo)
        {
            _applicationDbContext.Update(courseInfo);
        }
    }
}
