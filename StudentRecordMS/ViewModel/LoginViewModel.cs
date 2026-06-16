using System.ComponentModel.DataAnnotations;

namespace StudentRecordMS.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 30 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}
