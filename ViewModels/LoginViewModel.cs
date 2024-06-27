using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="Email OR UserName")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
