using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class BuyRepository : Repository<Buy>, IBuyRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public BuyRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

       //repository design pattern burada kullanıldı

        public void Update(Buy buy)
        {
            _applicationDbContext.Update(buy);
        }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
        public Buy Get(int id)
        {
            return _applicationDbContext.Buys.FirstOrDefault(c => c.Id == id);
        }
    }
}
