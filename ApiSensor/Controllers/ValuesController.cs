using FIWARE.OrionClient.IoTAgent;
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
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IHttpActionResult> Get()
        {
            RESTClient<string> restClient = new RESTClient<string>();
            string sensorEntityName = "urn:ngsi-ld:O2Sat:001";
            RootDataCatcher sensor = await restClient.GetMeasurementFromBroker(sensorEntityName);
            return Ok(sensor);
        }

        // GET api/values/5
        public string Get(int id)
        {            
            return WebApiConfig.GetValueFromAsyncClass().ToString();
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
