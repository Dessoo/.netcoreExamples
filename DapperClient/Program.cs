using DataAccessDapper.Models;
using DataAccessDapper.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DapperClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //IDbConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Test;User Id=Test; Password=DesoDesoKati1;");

            //IUserRepository userRepo = new UserRepository(con, nameof(User), new User().GetPrimaryKeys());

            ////userRepo.Delete(new Dictionary<string, string> { { "id", "1" } });
            //userRepo.Add(new User());

            try
            {
                var client = new HttpClient();
                //client.BaseAddress = new Uri("http://localhost:8090/api/v1/documentUploads/4230CAF8-DA9A-4B43-88EA-A0D2AB893737/content");
                client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlODg3MDFjZC1iM2QzLTQzNmMtODBjZi1kZWRmNTAyZDkzNzIiLCJqdGkiOiIyZWZkNDAwMS1hZmZiLTRiOGEtOTRhNS0yZDU5OTM3MjJmYjciLCJpc3MiOiJsb2NhbGhvc3Q6ODA5MCIsIm5iZiI6MTU0OTM1NTUwOSwiZXhwIjoxNTQ5NDQxOTA5LCJpYXQiOjE1NDkzNTU1MDl9.1KDBVhWaf1p-8WLzQvjrgZtlTqsdKj_i1a4JJvJfZqM");
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));


                var test = File.ReadAllBytes(@"C:\Users\deso\Desktop\ancora\AP_DEMO_KIT\AP_DEMO_KIT\Small AP Batch.pdf");
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8090/api/v1/documentUploads/4230CAF8-DA9A-4B43-88EA-A0D2AB893737/content");

                request.Content = new ByteArrayContent(test);

                var requestTest = client.PostAsync("http://localhost:8090/api/v1/documentUploads/4230CAF8-DA9A-4B43-88EA-A0D2AB893737/content", request.Content);
                Console.WriteLine("Response: {0}", requestTest);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }

            //keep alive this process 
            Console.WriteLine("Press <Enter> to exit... ");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
