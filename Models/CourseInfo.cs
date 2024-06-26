using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationManagementPlatform.Models
{
    public class CourseInfo//course modeli burada oluşturuldu
    {
        [Key]//pk
        public int Id { get; set; }
        [Required]
        public string? CourseName { get; set; }
        // [Required]
        public string Information { get; set; }
        [Required]
        public string? Owner { get; set; }
        [Required]
        [Range(10, 5000)]
        public double Price { get; set; }
        [Required]
        public double Time { get; set; }
        [ValidateNever]
        public int CourseCategoryId { get; set; }
        [ForeignKey("CourseCategoryId")]
        [ValidateNever]
        public CourseCategory? CourseCategory { get; set; }
        [ValidateNever]
        public string? File { get; set; }

    }
}
