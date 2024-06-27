using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class CreateProfileViewModel:ProfileViewModel
    {
        [Required(ErrorMessage = "Profile image Is Required")]

        public IFormFile ProfileImage { get; set; }
       
    }
}
