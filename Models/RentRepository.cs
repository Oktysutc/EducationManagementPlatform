using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class RentRepository : Repository<Rent>, IRentRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public RentRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

       //repository design pattern burada kullanıldı

        public void Update(Rent rent)
        {
            _applicationDbContext.Update(rent);
        }
        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
