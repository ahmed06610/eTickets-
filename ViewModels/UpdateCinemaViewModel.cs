namespace eTickets.ViewModels
{
    public class UpdateCinemaViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? LogoImage { get; set; }
    }
}
