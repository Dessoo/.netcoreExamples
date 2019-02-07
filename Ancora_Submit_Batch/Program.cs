using SsiServerCommunication;
using SsiServerCommunication.Clients.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ancora_Submit_Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            var tcpSettings = new SsiTcpClientSettings()
                {
                    ReceiveBufferSizeBytes = int.MaxValue/10,
                    ReceiveTimeoutMilliseconds = int.MaxValue,
                    SendBufferSizeBytes = int.MaxValue/10,
                    SendTimeoutMilliseconds = int.MaxValue
                };

            var ancoraConnection = SsiSessionFactory.CreateTcpClientSession("127.0.0.1", 12543, tcpSettings);
            //e88701cd-b3d3-436c-80cf-dedf502d9372 - user id
            //group_id="4e6d2bdf-7519-49fd-85ca-7626f73be1ad"
            var afterLogin = ancoraConnection.Login("Admin", "admin");
            //string userDetails = ancoraConnection.GetUserRoles("e88701cd-b3d3-436c-80cf-dedf502d9372");
            //string allRoles = ancoraConnection.GetRoles();
            try
            {
                //var test = ancoraConnection.GetBatchDetails("A8AB4E54-2942-49B0-9043-46B50000FB08");
                var test = ancoraConnection.WaitForDocumentDataCapture("3");
                Console.WriteLine(test);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
          
            ancoraConnection.Logout();

            //keep alive this process 
            Console.WriteLine("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
