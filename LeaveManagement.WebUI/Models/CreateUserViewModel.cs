using System.ComponentModel.DataAnnotations;

namespace LeaveManagement.WebUI.Models
{
    public class CreateUserViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Employee";

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*]).{6,}$",
        ErrorMessage = "Password must contain at least 6 characters, including uppercase, lowercase, number, and special character (!@#$%^&*).")]
        public string Password { get; set; } = string.Empty;
    }
}
