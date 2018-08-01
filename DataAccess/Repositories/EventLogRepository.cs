using DataAccess.Interfaces;
using DataAccess.Models;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EventLogRepository : EFRepository<EventLog>, IEventLogRepository
    {
        private readonly TestContext _context;

        public EventLogRepository(TestContext context)
            : base(context)
        {
            this._context = context;
        }
    }
}
