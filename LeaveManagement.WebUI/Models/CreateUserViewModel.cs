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
        [Display(Name = "Temporary Password")]
        public string Password { get; set; } = "Temp@12345";
    }
}
