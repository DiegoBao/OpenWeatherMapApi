using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Drakais.OpenWeatherMapApi
{
    public class OpenWeatherMapApiClient
    {
        HttpClient client;

        private string url;
        private string apiKey;
        private Units units;

        public OpenWeatherMapApiClient(string url, string apiKey, Units units)
        {
            this.url = url;
            this.apiKey = apiKey;
            this.units = units;

            this.Initialize();
        }

        private void Initialize()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);            
        }

        //Seaching by city name api.openweathermap.org/data/2.5/weather?q=London,uk

        public async Task<WeatherInfo> GetWeaterByCityNameAsync(string name)
        {
            var result = await client.GetAsync(string.Format("/data/2.5/weather?q={0}&units={1}", name, this.units.ToString().ToLowerInvariant()));
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Culture = new System.Globalization.CultureInfo("en-us");
                return JsonConvert.DeserializeObject<WeatherInfo>(json, settings);
            }
            else
            {
                return null;
            }
        }

        //Seaching by geographic coordinats api.openweathermap.org/data/2.5/weather?lat=35&lon=139
        //Seaching by city ID api.openweathermap.org/data/2.5/weather?id=2172797
    }
}
