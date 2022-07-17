using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using static System.Console;

namespace CoreCli
{
    class Program
    {
        const string uri = "http://311.austintexas.gov/open311/v2/requests.json";

        static async Task Main()
        {
            WriteLine("Connecting to the 311 API...\n");
            var response = await (new HttpClient()).GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                var items = JsonSerializer.Deserialize<List<Open311Item>>(result);

                WriteLine($"{"request_id", 11} {"status", 7} {"agency_responsible", 28} {"requested_datetime", 24}");
                foreach (var item in items)
                {
                    WriteLine($"{item.service_request_id, 11} {item.status, 7} {item.agency_responsible, 28} {item.GetRequestedDatetime(), 24}");
                }
            }
        }
    }
}