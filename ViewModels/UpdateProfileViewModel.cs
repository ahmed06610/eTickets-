namespace eTickets.ViewModels
{
    public class UpdateProfileViewModel:ProfileViewModel
    {
        public int Id { get; set; }
        public string? CurrentImage { get; set; }

        public IFormFile? ProfileImage { get; set; }

    }
}
