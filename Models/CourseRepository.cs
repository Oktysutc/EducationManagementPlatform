using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public CourseRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        public void Update(Course course)
        {
            _applicationDbContext.Update(course);
        }
    }
}
