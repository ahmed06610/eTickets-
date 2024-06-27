using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class ProfileViewModel
    {


        [Display(Name ="Name")]
        [Required(ErrorMessage ="Full Name Is Required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="The Name Must Be bettween 3 and 50")]
        public string FullName { get; set; }
        [Display(Name = "Bio Of The Actor")]
        [Required(ErrorMessage = "Bio Is Required")]

        public string Bio { get; set; }
    }
}
