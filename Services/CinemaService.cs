using eTickets.Data;
using eTickets.Models;
using eTickets.Services.Base;
using eTickets.Settings;
using eTickets.ViewModels;

namespace eTickets.Services
{
    public class CinemaService : GenericService<Cinema>, ICinemaService
    {
        public CinemaService(appdbcontext context) : base(context) { }
        
    }
}
