﻿using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.Models;
using System;

namespace SignalRClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "test.testov@bul.bg";
            string userName = "Test Testov";
            string authToken = "this!is!test!Token";
            string endPointUrl = $"http://localhost:5000/testHub?Email={email}&UserName={userName}&AuthToken={authToken}";

            var connection = new HubConnectionBuilder()
            .WithUrl(endPointUrl, HttpTransportType.WebSockets, options =>
            {
                options.SkipNegotiation = false;
            })
            .Build()
            .RetryConnect();

            connection.On<EventMessage>("SomeEvent", ( message) =>
            {
                Console.WriteLine($"{message.Message}");
            });
            
            try
            {
                connection.StartAsync();
                // connection.SendAsync("TestMethod", "Test input message");
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
