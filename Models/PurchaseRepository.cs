using EducationManagementPlatform.Utility;

namespace EducationManagementPlatform.Models
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
