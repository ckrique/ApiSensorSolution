using FIWARE.OrionClient.IoTAgent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.OrionClient
{
    public class RootServices
    {
        [JsonProperty("services")]
        public List<Service> services { get; set; }

        public RootServices()
        {
            services = new List<Service>();
        }
    }
}
