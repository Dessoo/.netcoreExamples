using Microsoft.Extensions.Logging;

namespace BusinessLayer.Core
{
    public interface ILogProvider
    {
        ILogger<TCategoryName> CreateLogger<TCategoryName>() where TCategoryName : class;

        bool UseQueue { get; set; }

        void Dispose();
    }
}
