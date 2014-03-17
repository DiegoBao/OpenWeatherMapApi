using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drakais.OpenWeatherMapApi
{
    public class Coordinates
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class Sys
    {
        public double message { get; set; }
        public string country { get; set; }
        [JsonConverter(typeof(Epoch2DateTime))]
        public DateTime Sunrise
        {
            get;
            set;
        }
        [JsonConverter(typeof(Epoch2DateTime))]
        public DateTime Sunset
        {
            get;
            set;
        }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("temp_min")]
        public double? MinTemperature { get; set; }
        [JsonProperty("temp_max")]
        public double? MaxTemperature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double? gust { get; set; }
        public double deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class WeatherInfo
    {
        public Coordinates coord { get; set; }
        public Sys sys { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
