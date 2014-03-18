using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Drakais.OpenWeatherMapApi;

namespace OpenWeatherMapApiTest
{
    [TestClass]
    public class ApiClientTest
    {
        OpenWeatherMapApiClient client;

        [TestInitialize]
        public void Initialize()
        {
            client = new Drakais.OpenWeatherMapApi.OpenWeatherMapApiClient("http://api.openweathermap.org/data/2.5/", "", Units.Metric);

        }

        [TestMethod]
        public void GetWeatherInfoByCity()
        {
            WeatherInfo wi = client.GetWeaterByCityNameAsync("Rubi,Barcelona,Spain").Result;

            Assert.IsNotNull(wi);
            Assert.AreEqual("Rubí", wi.Name);
            Assert.AreEqual(6, wi.sys.Sunrise.Value.Hour);
            Assert.AreEqual(57, wi.sys.Sunrise.Value.Minute);
            //Assert.AreEqual(11.88, wi.main.Temperature);
        }
    }
}
