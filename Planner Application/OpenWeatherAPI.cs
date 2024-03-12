using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Planner_Application
{
    //this class helps retrieve forecast data from the Open Weather API
    //this class uses the Singleton Design Pattern
    public class OpenWeatherAPI
    {
        //this private constructor ensures that the class will not be instantiated outside of this class
        private OpenWeatherAPI() {}

        //stores the singleton object
        private static OpenWeatherAPI _instance;

        //ensures that only one object is instantiated and returns reference to that object
        public static OpenWeatherAPI GetInstance()
        {
            if (_instance == null)
            {
                _instance = new OpenWeatherAPI();
            }
            return _instance;
        }

        //retrieves forecast data from Open Weather API based on city parameter
        public async Task<OpenWeatherResponse> RequestData(String city)
        {
            //uses HTTPClient to help retrieve data
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/forecast?q={city}&units=metric&appid=21cb86ae143a611e1f80bb8360b3e49e");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawForecast = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);

                    return rawForecast;
                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine(httpRequestException.Message);
                    return new OpenWeatherResponse();
                }
            }
        }

        //this class is used for assisting in deserializing object
        public class OpenWeatherResponse
        {
            public CityDescription City { get; set; }

            public List<ListDescription> List { get; set; }
        }

        //this class is used for assisting in deserializing object
        public class CityDescription
        {
            public string Name { get; set; }

            public string Country { get; set; }
        }

        //this class is used for assisting in deserializing object
        public class ListDescription
        {
            public string dt_txt { get; set; }

            public MainDescription Main { get; set; }

            public List<WeatherDescription> Weather { get; set; }
        }

        //this class is used for assisting in deserializing object
        public class MainDescription
        {
            public string temp { get; set; }
        }

        //this class is used for assisting in deserializing object
        public class WeatherDescription
        {
            public string main { get; set; }

            public string description { get; set; }
        }


    }
}
