using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EducationManagementPlatform.Models
{
    public class AppUser : IdentityUser
    {
        // ek kullanıcı sınııfı  burada oluşturuldu
       // public int Ogrencino { get; set; }
       [Required]
        public string? Address { get; set; }
        public string? Faculty { get; set; }
        public string? Episode { get; set; }
    }
}
