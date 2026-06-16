using System.ComponentModel.DataAnnotations;

namespace StudentRecordMS.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Maths marks are required.")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100.")]
        public int? Maths { get; set; }

        [Required(ErrorMessage = "Physics marks are required.")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100.")]
        public int? Physics { get; set; }

        [Required(ErrorMessage = "Chemistry marks are required.")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100.")]
        public int? Chemistry { get; set; }

        [Required(ErrorMessage = "English marks are required.")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100.")]
        public int? English { get; set; }

        [Required(ErrorMessage = "Programming marks are required.")]
        [Range(1, 100, ErrorMessage = "Marks must be between 1 and 100.")]
        public int? Programming { get; set; }

        public bool IsActive { get; set; } = true;
        public int? UserId { get; set; }
    }
}
