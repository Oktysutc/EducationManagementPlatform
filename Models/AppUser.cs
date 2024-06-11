using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EducationManagementPlatform.Models
{
    public class AppUser : IdentityUser
    {
        [Required]// ek kullanıcı sınııfı  burada oluşturuldu
        public int Ogrencino { get; set; }
        public string? Address { get; set; }
        public string? Faculty { get; set; }
        public string? Episode { get; set; }
    }
}
