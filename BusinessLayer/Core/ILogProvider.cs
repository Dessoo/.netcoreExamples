using Microsoft.Extensions.Logging;

namespace BusinessLayer.Core
{
    public interface ILogProvider
    {
        ILogger<TCategoryName> CreateLogger<TCategoryName>() where TCategoryName : class;

        void Dispose();
    }
}
