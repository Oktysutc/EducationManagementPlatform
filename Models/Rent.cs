using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace EducationManagementPlatform.Models
{
    public class Rent
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
        public int RentPrice { get; set; }
        public string RentName { get; set; }


    }
}
