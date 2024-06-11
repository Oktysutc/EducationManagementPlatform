using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EducationManagementPlatform.Models
{
    public class Buy//satın alma sayfasının modeli
    {
        [Key]//pk
        public int Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        // [Required]
        [ValidateNever]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [ValidateNever]
        public Course Course { get; set; }
        public int SalesPrice { get; set; }
    }
       }
