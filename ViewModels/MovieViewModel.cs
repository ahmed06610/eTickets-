using eTickets.Data.Enums;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModels
{
    public class MovieViewModel
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name Must Be bettween 3 and 50")]

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Display(Name ="Category")]
        public MovieCategory MovieCategory { get; set; }
        [Display(Name = "the Actors")]

        public List<int> ActorIds { get; set; }

        [Display(Name = "Cinema")]

        public int CinemaId { get; set; }
        [Display(Name = "the Producer")]

        public int ProducerId { get; set; }



    }
}
