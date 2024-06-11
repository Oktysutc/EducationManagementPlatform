using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducationManagementPlatform.Models
{
    public class CourseCategory //kurs ların kategori sınıfı burada oluşturuldu
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="not null")]
        [MaxLength(25)]
        [DisplayName("kategori adı")]
        public string Name { get; set; }
    }
}
