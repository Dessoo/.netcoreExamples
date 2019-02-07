using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IvoAppApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = (System.Net.HttpWebRequest)WebRequest.Create("https://api.suredone.com/v1/search/items/16702-230-1219");

            request.Headers.Add("X-Auth-User", "viktor");
            request.Headers.Add("X-Auth-Token", "41244227626DC7863EFC9A0612319D7158E3BA34DE33FBDF9C02DA4E99005107486357D22D5257B415LAWADOTWIJLUKG0X0TN9BDBQA4IQ");
            request.Method = "GET";
            request.ContentType = "multipart/form-data";

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
