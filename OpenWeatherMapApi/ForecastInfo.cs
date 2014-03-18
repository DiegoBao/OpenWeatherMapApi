using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drakais.OpenWeatherMapApi
{
    //public class Coord
    //{
    //    public double lon { get; set; }
    //    public double lat { get; set; }
    //}

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonProperty("coord")]
        public Coordinates Coordinates { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public int Hours3 { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("dt"), JsonConverter(typeof(Epoch2DateTime))]
        public int Timestamp { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public string TimestampText { get; set; }
        public Rain rain { get; set; }
    }

    public class ForecastInfo
    {
        public string Code { get; set; }
        public double Message { get; set; }
        public City City { get; set; }
        //public int cnt { get; set; }
        [JsonProperty("list")]
        public List<Forecast> Forecasts { get; set; }
    }
}
