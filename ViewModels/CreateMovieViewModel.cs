using eTickets.Data.Enums;
using eTickets.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.ViewModels
{
    public class CreateMovieViewModel:MovieViewModel
    {
        public IFormFile MovieImage { get; set; }


    }
}
