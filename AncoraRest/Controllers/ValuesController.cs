using Elders.Web.Api;
using Newtonsoft.Json;
using SsiServerCommunication;
using SsiServerDto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace AncoraRest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(string id)
        {
            var ancoraConnection = SsiSessionFactory.CreateTcpClientSession("127.0.0.1", 12543, null);

            var test = new Ssi.Communication.TCPCommunication.TCPCommunication();
            return Ok(new ResponseResult<BatchInfo>(""));
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
