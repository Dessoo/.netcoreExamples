using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class EventLogRepository : EFRepository<EventLog>, IEventLogRepository
    {
        public EventLogRepository(TestContext context)
            : base(context)
        {
            //inject other repositories instance here 
        }
    }
}
