using DataAccess.Models;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEventLogRepository
    {
        void Add(EventLog eventLog);

        Task<int> AddAsync(EventLog eventLog);
    }
}
