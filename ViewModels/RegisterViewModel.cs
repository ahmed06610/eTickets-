using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name ="Full Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,100}$", ErrorMessage = "Passwords must have at least one lowercase ('a'-'z'), one uppercase ('A'-'Z'), and one digit ('0'-'9').")]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords Do Not Match")]
        [Display(Name = "Confirm Password")]


        public string ConfirmPassword { get; set; }
    }
}
