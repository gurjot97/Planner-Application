using Newtonsoft.Json;
using static Planner_Application.OpenWeatherAPI;
using Newtonsoft.Json;

namespace Planner_Application
{
    //this class helps retrieve happy quotes
    //this class uses the Template Method Design Pattern
    //this is a derived concrete class that inherits from the Quote base class
    public class HappyQuote : Quote
    {
        //this function helps retrieve happy quotes
        //this function hook overrides the base class function and provides implementation
        protected override string getQuote()
        {
            return RequestData().Result.quote;
        }

        //retrieves happy quote data from API Ninjas
        public async Task<HappyQuoteResponse> RequestData()
        {
            //uses HTTPClient to help retrieve data
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.api-ninjas.com");
                    client.DefaultRequestHeaders.Add("X-Api-Key", "CIElpFUr1VovCuSg/qWmSw==asigoCZfXjwH5smy");
                    var response = await client.GetAsync($"/v1/quotes?category=happiness");
                    response.EnsureSuccessStatusCode();

                    var stringResult = (await response.Content.ReadAsStringAsync()).Replace("[", "").Replace("]", "");

                    var rawForecast = JsonConvert.DeserializeObject<HappyQuoteResponse>(stringResult);

                    return rawForecast;
                }
                catch (HttpRequestException httpRequestException)
                {
                    Console.WriteLine(httpRequestException.Message);
                    return new HappyQuoteResponse();
                }
            }
        }

        //this class is used for assisting in deserializing object
        public class HappyQuoteResponse
        {
            public string quote { get; set; }
        }
    }
}
