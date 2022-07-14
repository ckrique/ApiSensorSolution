using FIWARE.OrionClient.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiSensor.Controllers
{
    public class SensorProvisionToBrokerController : ApiController
    {
        // GET: api/sensorProvisionToBroker
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/sensorProvisionToBroker/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/sensorProvisionToBroker
        public async Task<IHttpActionResult> PostAsync(/*[FromBody]string value*/)
        {
            RESTClient<string> restClient = new RESTClient<string>();
            Task<string> returnedTask = restClient.SensorProvisionToBrokerController();
            string returnedValue = await returnedTask;
            //create Conflict return abd send it if necessary
            return Ok(returnedValue);
        }

        // PUT: api/sensorProvisionToBroker/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/sensorProvisionToBroker/5
        public void Delete(int id)
        {
        }
    }
}
