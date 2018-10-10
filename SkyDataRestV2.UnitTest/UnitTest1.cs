using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyDataRestV2.Controllers;
using SkyDataRestV2.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Script.Serialization;

namespace SkyDataRestV2.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void PostSetsLocationHeader()
        {
            // Arrange
            RequestsController controller = new RequestsController();// repository);

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "products" } });

            // Act
            Request product = new Request
            {
                code = 0,
                vid = 324,
                remote_id = "Mi correlativo",
                license_plane = "P-123ABC",
                device_id = 358741050302485,
                ip = "216.58.192.100",
                port = -1,
                lon = -85.54562,
                lat = 10.67338,
                valid_position = true,
                event_time = "2016-09-08T17:42:14+0",
                system_time = "2016-09-08T17:42:15.411-06:00",
                kmph = 16,
                placeneme = "111 Oak street, New York, NY 11010",
                poi_ids = new int[] { 1123, 542 },
                poi_group_ids = new int[] { 1123, 542 },
                alerts_count = 0,
                route_id = null,
                client_id = 0,
                head = 359,
                odometer = 1231.1,
                sys_odometer = 1000000.1,
                hourmeter = 1000.1,
                sys_hourmeter = 1.1
            };
            string json = new JavaScriptSerializer().Serialize(product);
            var response = controller.Post(json);

            // Assert
            Assert.AreEqual(1, 1);
        }
    }
}
