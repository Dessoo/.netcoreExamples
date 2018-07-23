using DataAccess.Models;

namespace DataAccess.Interfaces
{
    public interface IEventLogRepository
    {
        void Add(EventLog eventLog);

        void AddAsync(EventLog eventLog);
    }
}
