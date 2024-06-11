using System.Linq.Expressions;

namespace EducationManagementPlatform.Models
{
    public interface IRepository <T> where T:class
    {//repositorylerin interfacesi buarada kullanıldı
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get (Expression<Func<T, bool>> filtre, string? includeProps = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T>entities);
    }
}
