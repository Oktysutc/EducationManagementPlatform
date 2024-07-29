using EducationManagementPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationManagementPlatform.Utility
{
    public class ApplicationDbContext : IdentityDbContext
    {//veri tabanıyla bağlantımızı sağlayan köprü sınıfı buarada olusştuurldu

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }// bu kısa kod köprü görevi görür
        // modelleri buaraya yazmazsam veri tabanına atmamam
        public DbSet<CourseCategory> CourseCategories { get; set; }//kurs kategori
        public DbSet<Course> Courses { get; set; }//kurs
        public DbSet<Buy> Buys { get; set; }//satın alma işlemleri
       
        public DbSet<AppUser> AppUsers { get; set; }// kullanıcı işlemleri
       
       public DbSet<Purchase> Purchases { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Price property için store type belirleyin
            modelBuilder.Entity<Purchase>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Diğer yapılandırmalar...
        }



    }


}
