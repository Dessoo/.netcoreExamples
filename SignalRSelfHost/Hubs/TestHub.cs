using BusinessLayer.Core;
using Microsoft.Extensions.Logging;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Infrastructure;

namespace SignalRSelfHost.Hubs
{
    [HubName("TestHub")]
    public class TestHub : BaseHub<TestHub>, ITestHub
    {
        private readonly ILogger _logger;

        public TestHub(IConnectionManager connectionManager, ILogProvider logProvider) : base(connectionManager, logProvider)
        {
            this._logger = logProvider.CreateLogger<TestHub>();      
        }

        public void TestMethod(string testMessage)
        {
            //inject message from client to server
            _logger.LogDebug(testMessage);
        }
    }
}
