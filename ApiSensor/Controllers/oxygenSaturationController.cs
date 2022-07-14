using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiSensor.Controllers
{
    public class oxygenSaturationController : ApiController
    {
        // GET: api/oxygenSaturation
        public string Get()
        {
            //return "oxygenSaturationPercentage | 22";
            return WebApiConfig.GetValueFromAsyncClass().ToString();
        }

        // GET: api/oxygenSaturation/5
        public string Get(int id)
        {
            return "value";
        }
        

        // POST: api/oxygenSaturation
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/oxygenSaturation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/oxygenSaturation/5
        public void Delete(int id)
        {
        }
    }
}
