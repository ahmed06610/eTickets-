using eTickets.Data.Enums;
using eTickets.Models;

namespace eTickets.ViewModels
{
    public class UpdateMovieViewModel:MovieViewModel
    {
        public int Id { get; set; }
       
        public IFormFile? MovieImage { get; set; }
        public string? img { get; set; }
        
    }
}
