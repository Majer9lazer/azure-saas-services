using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
namespace TimerTrigger
{
    public static class WeatherMapApiFunction
    {
        private const string BaseAddress = "https://samples.openweathermap.org/data/2.5/weather?";
        private const string AppId = "";
        private const string Almaty = "Almaty";
        private const string Astana = "Astana";
        private const string Chimkent = "Chimkent";
        private static readonly Random _random = new Random();
        [FunctionName("WeatherMapApiFunction")]
        public static void Run([TimerTrigger("*/3 * * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            string randomCity = "";
            switch (_random.Next(0, 2))
            {
                case 0:
                    randomCity = Almaty; break;
                case 1:
                    randomCity = Astana; break;
                case 2:
                    randomCity = Chimkent; break;
            }
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            HttpClient client = new HttpClient();
            string query = $"{BaseAddress}q={randomCity}&appid={AppId}";
            RootObject rootObject = new RootObject();
            HttpResponseMessage httpResponseMessage = client.GetAsync(query).GetAwaiter().GetResult();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                rootObject = httpResponseMessage.Content.ReadAsAsync<RootObject>().GetAwaiter().GetResult();
                log.Info(httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
        }
    }
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
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
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
