using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRSelfHost.Hubs
{
    public interface ITestHub
    {
        void TestMethod(string testMessage);
    }
}
