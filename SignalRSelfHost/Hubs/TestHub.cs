using BusinessLayer.Core;
using SignalRSelfHost.Connection;
using SignalRSelfHost.Infrastructure;

namespace SignalRSelfHost.Hubs
{
    [HubName("TestHub")]
    public class TestHub : BaseHub<TestHub>
    {
        public TestHub(IConnectionManager connectionManager, ILogProvider logProvider) : base(connectionManager, logProvider)
        {

        }                   
    }
}
