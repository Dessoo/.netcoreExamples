using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AncoraInputClient.Services
{
    public class RequestService
    {
        private readonly HttpClient _requestObject;

        public RequestService(Uri endPoint)
        {
            this._requestObject = new HttpClient();
            this._requestObject.BaseAddress = endPoint;
            this._requestObject.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Post()
        {

            //// List data response.
            //HttpResponseMessage response = this._requestObject.PostAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body.
            //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            //    foreach (var d in dataObjects)
            //    {
            //        Console.WriteLine("{0}", d.Name);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}

            //Make any other calls using HttpClient here.

            //Dispose once all 
        }
    }
}
