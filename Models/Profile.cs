using eTickets.Services.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public abstract class Profile :IGService
    {
        [Key]
        public int Id { get; set; }
        public string? ProfileImage { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
    }
}
