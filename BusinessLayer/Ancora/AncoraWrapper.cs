using System;
using System.Collections.Generic;
using System.Text;
using SsiServerCommunication;

namespace BusinessLayer.Ancora
{
    public class AncoraWrapper
    {
        private ISsiClientSession _ancoraConnection;

        public AncoraWrapper()
        {
            this._ancoraConnection = SsiSessionFactory.CreateTcpClientSession("127.0.0.1", 12543, null);
            this._ancoraConnection.Login("admin", "admin");
        }

        public string GetBatchDetails(string batchId)
        {
            return this._ancoraConnection.GetBatchDetails(batchId);        
        }
    }
}
