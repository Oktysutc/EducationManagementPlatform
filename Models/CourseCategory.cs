using System.ComponentModel.DataAnnotations;

namespace EducationManagementPlatform.Models
{
    public class CourseCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
