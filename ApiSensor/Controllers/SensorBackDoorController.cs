using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSensor.Controllers
{
    public class SensorBackDoorController : ApiController
    {
        // GET: api/BackDoor
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BackDoor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BackDoor
        public HttpResponseMessage Post([FromBody]string value)
        {
            IEnumerable<KeyValuePair<string, string>> parChaveValor = Request.GetQueryNameValuePairs();
            string direction = "";

            foreach (KeyValuePair<string, string> kvp in parChaveValor)
            {
                if (kvp.Key.ToString().Equals("direction"))
                    direction = kvp.Value.ToString();                
            }
            
            if (direction.ToUpper().Equals("UP"))
                WebApiConfig.ChangeDirectionSensorValueToUp();
            else if (direction.Equals("DOWN"))
                WebApiConfig.ChangeDirectionSensorValueToDown();

            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

            // PUT: api/BackDoor/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BackDoor/5
        public void Delete(int id)
        {
        }
    }
}
