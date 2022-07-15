using FIWARE.OrionClient.IoTAgent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.OrionClient.REST
{
    public class RESTClient<T>
    {
        private string AuthHeaderKey;
        private string AuthToken;

        public RESTClient()
        {

        }

        /// <summary>
        /// Creates a new instance of the RESTClient with authentication information
        /// </summary>
        /// <param name="authHeaderKey"></param>
        /// <param name="authToken"></param>
        public RESTClient(string authHeaderKey, string authToken)
        {
            this.AuthHeaderKey = authHeaderKey;
            this.AuthToken = authToken;
        }

        /// <summary>
        /// Retrieves the date from the provided URI and returns it as an object of type T
        /// </summary>
        /// <param name="uri">The URL to retrieve</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Posts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to post to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PostAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Posts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to post to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<string> PostAsyncIoTAgentInitializationAsync(string uri/*, string body*/)
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            RootServices rootServices = new RootServices();
            Service service = new Service();

            service.apikey = "4jggokgpepnvsb2uv4s40d59ov";
            service.cbroker = "http://orion:1026";
            service.entity_type = "Thing";
            service.resource = "/iot/d";

            rootServices.services.Add(service);

            string body = JsonConvert.SerializeObject(rootServices, jsonSettings);
            var data = new StringContent(body, Encoding.UTF8, "application/json");
            var url = "http://localhost:4041/iot/services";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("fiware-service", "openiot");
            client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public async Task<string> SensorProvisionToBrokerController()
        {
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            RootDevices rootDevices = new RootDevices();
            Device sensorDevice = new Device();
            sensorDevice.device_id = "sensor001";
            sensorDevice.entity_name = "urn:ngsi-ld:sensor:001";
            sensorDevice.entity_type = "O2Sat";

            FIWARE.OrionClient.IoTAgent.Attribute attribute = new FIWARE.OrionClient.IoTAgent.Attribute();
            attribute.object_id = "s";
            attribute.name = "O2Saturation";
            attribute.type = "float";
            
            sensorDevice.attributes.Add(attribute);
            rootDevices.devices.Add(sensorDevice);

            using (var client = new HttpClient())
            {
                string body = JsonConvert.SerializeObject(rootDevices, jsonSettings);
                var data = new StringContent(body, Encoding.UTF8, "application/json");
                var url = "http://localhost:4041/iot/devices";


                client.DefaultRequestHeaders.Add("fiware-service", "openiot");
                client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                HttpResponseMessage clientResponse = await client.PostAsync(url, data);

                if (clientResponse.IsSuccessStatusCode)
                {
                    //var content = await clientResponse.Content.ReadAsStringAsync();

                    //T genericResponse = JsonConvert.DeserializeObject<T>(content);
                    //return genericResponse.ToString();

                    string result = clientResponse.Content.ReadAsStringAsync().Result;
                    return result;
                }
                else
                {
                    throw new Exception(clientResponse.ReasonPhrase);
                }
            }
        }

        public async Task<string> SendMeasurementFromSensorToBroker(float measuredValue)
        {
            using (var client = new HttpClient())
            {
                string body = "s|" + measuredValue.ToString();
                var data = new StringContent(body, Encoding.UTF8, "text/plain");
                string token = "4jggokgpepnvsb2uv4s40d59ov";
                string url = string.Format("http://localhost:7896/iot/d?k={0}&i=sensor001", token);

                HttpResponseMessage clientResponse = await client.PostAsync(url, data);

                if (clientResponse.IsSuccessStatusCode)
                {
                    //var content = await clientResponse.Content.ReadAsStringAsync();

                    //T genericResponse = JsonConvert.DeserializeObject<T>(content);
                    //return genericResponse.ToString();

                    string result = clientResponse.Content.ReadAsStringAsync().Result;
                    return result;
                }
                else
                {
                    throw new Exception(clientResponse.ReasonPhrase);
                }
            }
        }

        public async Task<RootDataCatcher> GetMeasurementFromBroker(string entityName)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("http://localhost:1026/v2/entities/{0}", entityName);


                client.DefaultRequestHeaders.Add("fiware-service", "openiot");
                client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                HttpResponseMessage clientResponse = await client.GetAsync(url);

                string result = clientResponse.Content.ReadAsStringAsync().Result;

                RootDataCatcher sensor = JsonConvert.DeserializeObject<RootDataCatcher>(result);

                return sensor;
            }

        }


        /// <summary>
        /// Puts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to put to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PutAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Deletes the date at the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to delete</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
