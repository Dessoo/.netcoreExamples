using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRSelfHost.Connection
{
    public interface IConnectionManager
    {
        void AddConnection(string hubName, string connectionId, AuthEntity authEntity);

        void DeleteConnection(string hubName, string connectionId);

        List<string> GetActiveConnectionForHub(string hubName);

        List<string> GetActiveConnectionForUserPerHub(string hubName, string email);
    }
}
