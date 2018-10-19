using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace RiadosaOrg.Controllers.API
{
    public class WeatherChimeController : ApiController
    {
        
        [System.Web.Http.HttpPost]
        public string AutoComp(string searchText)
        {
            var result = "";

            var param = "aq?query=" + searchText;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://autocomplete.wunderground.com/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(param).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                result = response.Content.ReadAsStringAsync().Result;

                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d.Name);
                //}
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return result;
        }   
    }
}