using eTickets.Data;
using eTickets.Models;
using eTickets.Services.Base;
using eTickets.Settings;
using eTickets.ViewModels;

namespace eTickets.Services
{
    public class ActorService:GenericService<Actor>,IActorService
    {
        public ActorService(appdbcontext context):base(context)
        {
            
        }
    }
}
