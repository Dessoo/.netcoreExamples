using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace SignalRClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "test.testov@bul.bg";
            string userName = "Test Testov";
            string authToken = "this!is!test!Token1";

            var connection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:5000/testHub?Email={email}&UserName={userName}&AuthToken={authToken}", HttpTransportType.WebSockets, options =>
            {
                options.SkipNegotiation = false;
            })
            .Build();

            connection.On<string>("SomeEvent", ( message) =>
            {
                Console.WriteLine($"{message}");
            });

            try
            {
                connection.StartAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //keep alive this process 
            Console.WriteLine("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
