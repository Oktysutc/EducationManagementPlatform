using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class CourseCategoryRepository : Repository<CourseCategory>, ICourseCategoryRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public CourseCategoryRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }
        //repository design pattern burada kullanıldı
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(CourseCategory courseCategory)
        {
            _applicationDbContext.Update(courseCategory);
        }
    }
}
