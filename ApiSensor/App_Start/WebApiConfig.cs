using ApiSensor.ArcaboucoSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ApiSensor
{
    public static class WebApiConfig
    {
        public static AsyncClass asyncClass = new AsyncClass();

        public static void Register(HttpConfiguration config)
        {
            // Ações do Sensor
            
            asyncClass.StartThread();

            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static double GetValueFromAsyncClass()
        {
            return asyncClass.GetSensorValue();
        }

        public static void ChangeDirectionSensorValueToDown()
        {
            asyncClass.ChangeDirectionSensorValueToDown();
        }

        public static void ChangeDirectionSensorValueToUp()
        {
            asyncClass.ChangeDirectionSensorValueToUp();
        }
    }
}
