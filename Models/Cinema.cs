using eTickets.Services.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema:IGService
    {
        [Key]
        public int Id { get; set; }
        public string? LogoImage { get; set; }
        public string Name { get; set; }
        [Display(Name ="Location")]
        public string Description { get; set; }
        public List<Movie>? Movies { get; set; } 
    }
}
