using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.OrionClient.IoTAgent
{
    public class Device
    {
        [JsonProperty("device_id")]
        public string device_id { get; set; }

        [JsonProperty("entity_name")]
        public string entity_name { get; set; }

        [JsonProperty("entity_type")]
        public string entity_type { get; set; }

        [JsonProperty("timezone")]
        public string timezone { get; set; }

        [JsonProperty("attributes")]
        public List<Attribute> attributes { get; set; }

        [JsonProperty("static_attributes")]
        public List<StaticAttribute> static_attributes { get; set; }

        public Device()
        {
            attributes = new List<Attribute>();
            static_attributes = new List<StaticAttribute>();
        }
    }
}
