namespace EducationManagementPlatform.Models
{
    public interface IBuyRepository :IRepository<Buy>
    {//repositorylerin interface leri burada kullanıldı
        void Update(Buy buy);
        void Save();
        Buy Get(int id);
    }
}
