namespace EducationManagementPlatform.Models
{
    public interface IRentRepository :IRepository<Rent>
    {//repositorylerin interface leri burada kullanıldı
        void Update(Rent rent);
        void Save();
    }
}
