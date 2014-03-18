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
        private string language;

        public OpenWeatherMapApiClient(string url, string apiKey, Units units = Units.Metric, string language = null)
        {
            this.url = url;
            this.apiKey = apiKey;
            this.units = units;
            this.language = language;

            this.Initialize();
        }

        private void Initialize()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            if (!string.IsNullOrWhiteSpace(this.apiKey))
            {
                client.DefaultRequestHeaders.Add("x-api-key", this.apiKey);
            }
        }

        //Seaching by city name api.openweathermap.org/data/2.5/weather?q=London,uk

        public async Task<WeatherInfo> GetWeaterByCityNameAsync(string name)
        {
            string url = string.Format("/data/2.5/weather?q={0}&units={1}", name, this.units.ToString().ToLowerInvariant());
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetWeatherInfo(result);
        }

        private static async Task<WeatherInfo> GetWeatherInfo(HttpResponseMessage result)
        {
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

        public async Task<WeatherInfo> GetWeatherByLocationAsync(double longitude, double latitude)
        {
            //Seaching by geographic coordinats api.openweathermap.org/data/2.5/weather?lat=35&lon=139
            string url = string.Format("/data/2.5/weather?lat={0}&long={1}&units={2}", latitude, longitude, this.units.ToString().ToLowerInvariant());
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetWeatherInfo(result);
        }
        
        public async Task<WeatherInfo> GetWeatherByCityIdAsync(int cityId)
        {
            //Seaching by city ID api.openweathermap.org/data/2.5/weather?id=2172797
            string url = string.Format("/data/2.5/weather?id={0}&units={1}", cityId, this.units.ToString().ToLowerInvariant());
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetWeatherInfo(result);
        }

        public async Task<ForecastInfo> GetForecastByCityAsync(string city, int? days = null)
        {
            string url = string.Format("/data/2.5/forecast?q={0}&units={1}", city, this.units.ToString().ToLowerInvariant());
            if (days.HasValue)
            {
                url = string.Format("{0}&cnt={1}", url, days.Value);
            }
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetForecastInfo(result);
        }

        public async Task<ForecastInfo> GetForecastByCityIdAsync(int cityId, int? days = null)
        {
            string url = string.Format("/data/2.5/forecast?id={0}&units={1}", cityId, this.units.ToString().ToLowerInvariant());
            if (days.HasValue)
            {
                url = string.Format("{0}&cnt={1}", url, days.Value);
            }
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetForecastInfo(result);
        }

        public async Task<ForecastInfo> GetForecastByLocationAsync(double latitude, double longitude, int? days = null)
        {
            string url = string.Format("/data/2.5/forecast?lat={0}&long={1}&units={2}", latitude, longitude, this.units.ToString().ToLowerInvariant());
            if (days.HasValue)
            {
                url = string.Format("{0}&cnt={1}", url, days.Value);
            }
            url = this.AddLanguage(url);
            var result = await client.GetAsync(url);
            return await GetForecastInfo(result);
        }

        private string AddLanguage(string url)
        {
            if (!string.IsNullOrWhiteSpace(this.language))
            {
                return string.Format("{0}&lang={1}", url, this.language);
            }
            else
            {
                return url;
            }
        }

        private async Task<ForecastInfo> GetForecastInfo(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                var json = await result.Content.ReadAsStringAsync();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Culture = new System.Globalization.CultureInfo("en-us");
                return JsonConvert.DeserializeObject<ForecastInfo>(json, settings);
            }
            else
            {
                return null;
            }
        }
    }
}
