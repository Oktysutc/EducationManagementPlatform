using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EducationManagementPlatform.Models
{
    public class Buy
    {
        [Key]//pk
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        // [Required]
        public string Info { get; set; }
        [Required]
        public string Point { get; set; }
        [Required]
        [Range(10, 5000)]
        public double SalesPrice { get; set; }
        }
}
