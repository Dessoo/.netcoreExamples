using System;

namespace SignalRSelfHost.Infrastructure
{
    internal class HubNameAttribute : Attribute
    {
        public readonly string _hubName;
        
        public HubNameAttribute(string hubName)
        {
            this._hubName = hubName;
        }
    }
}
