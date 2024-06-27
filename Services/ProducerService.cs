using eTickets.Data;
using eTickets.Models;
using eTickets.Services.Base;
using eTickets.Settings;
using eTickets.ViewModels;

namespace eTickets.Services
{
    public class ProducerService : GenericService<Producer>, IProducerService
    {
        public ProducerService(appdbcontext context):base(context)
        {
            
        }
    }

}
