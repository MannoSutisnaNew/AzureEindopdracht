using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageQueue.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;
using System.IO;

namespace ImageQueue.Helpers
{
    class RequestHelper
    {
        public RequestHelper()
        {

        }

        public static Coordinates getCoordinates(ImageQueueMessage message)
        {
            string address = message.street + " " + message.houseNumber + ", " + message.residence;
            string encodedAddress = HttpUtility.UrlEncode(address);
            string URL = "https://maps.googleapis.com/maps/api/geocode/json";
            string addressParameter = "?address=" + encodedAddress;
            string geoCodeApiKey = Environment.GetEnvironmentVariable("GeoCodeApiKey");
            string keyParameter = "&key=" + geoCodeApiKey;
            string completeURL = URL + addressParameter + keyParameter;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(addressParameter + keyParameter).Result; 
            if (response.IsSuccessStatusCode)
            {
                string res = null;
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = @result.Result;
                }

                dynamic o = JsonConvert.DeserializeObject(res);
                string lng = o.results[0].geometry.location.lng;
                string lat = o.results[0].geometry.location.lat;
                return new Coordinates(lng, lat);
            }
            else
            {
                return null;
            }
        }

        public static async Task<double> TemperatureRequest(Coordinates coordinates)
        {
            string URL = "https://api.openweathermap.org/data/2.5/weather";
            string latitudeParameter = "?lat=" + coordinates.latitude;
            string longitudeParameter = "&lon=" + coordinates.longitude;
            string unitsParameter = "&units=metric";
            string openWeatherApiKey = Environment.GetEnvironmentVariable("OpenWeatherApiKey");
            string keyParameter = "&APPID=" + openWeatherApiKey;
            string allParameters = latitudeParameter + longitudeParameter + unitsParameter + keyParameter;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(allParameters);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string res = null;
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    Task<string> result = content.ReadAsStringAsync();
                    res = @result.Result;
                }
                dynamic o = JsonConvert.DeserializeObject(res);
                string temperatureString = o.main.temp;
                double temperature = 0;
                try
                {
                    temperature = Convert.ToDouble(temperatureString);
                }
                catch
                {
                    return -1000;
                }
                return temperature;
            }
            else
            {
                return -1000;
            }
        }

        public static async Task<Stream> ImageRequest(Coordinates coordinates)
        {
            string URL = "https://atlas.microsoft.com/map/static/png";
            string mapsApiKey = Environment.GetEnvironmentVariable("MapsApiKey");
            string subscriptionParameter = "?subscription-key=" + mapsApiKey;
            string apiParameter = "&api-version=1.0";
            string layerParameter = "&layer=basic";
            string styleParameter = "&style=main";
            string zoomParameter = "&zoom=16";
            string centerParameter = "&center=" + coordinates.longitude + "," + coordinates.latitude;
            string allParameters = subscriptionParameter + apiParameter + layerParameter + styleParameter + zoomParameter + centerParameter;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // List data response.
            HttpResponseMessage response = await client.GetAsync(allParameters);  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {  
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                return responseStream;
            }
            else
            {
                return null;
            }
        }
    }
}
