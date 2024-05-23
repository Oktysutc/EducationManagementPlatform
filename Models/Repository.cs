using EducationManagementPlatform.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationManagementPlatform.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ApplicationDbContext _applicationDbContext;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this.dbSet=_applicationDbContext.Set<T>();
            _applicationDbContext.Courses.Include(k => k.CourseCategory).Include(k => k.CourseCategoryId);


        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public T Get(Expression<Func<T, bool>> filtre,string? includeProps=null)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu=sorgu.Where(filtre);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeprop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeprop);
                }
            }
            return sorgu.FirstOrDefault();
        }
        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> sorgu=dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeprop in includeProps.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    sorgu=sorgu.Include(includeprop);
                }
            }
            return sorgu.ToList();
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }


        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

       

       
    }
}
