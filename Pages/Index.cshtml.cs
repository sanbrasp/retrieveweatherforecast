using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace RetrieveWeatherForecast.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? myData { get; set; }

        public async Task OnGet()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44382/weatherforecast");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            myData = "No weather report today";
            HttpResponseMessage getData = await client.GetAsync("https://localhost:44382/weatherforecast");

            if (getData.IsSuccessStatusCode)
            {
                String data = getData.Content.ReadAsStringAsync().Result;

                WeatherForecast[]? JSONData = JsonSerializer.Deserialize<WeatherForecast[]>(data);

                myData = "";
                foreach (WeatherForecast Item in JSONData)
                {
                    myData += "<tr>";
                    myData += "<td>" + Item.date.ToLongDateString() + "</td>";
                    myData += "<td>" + Item.temperatureC + "</td>";
                    myData += "<td>" + Item.temperatureF + "</td>";
                    myData += "<td>" + Item.summary + "</td>";
                    myData += "</tr>";
                }
            }

            await Task.Delay(1000);
        }
    }
}
