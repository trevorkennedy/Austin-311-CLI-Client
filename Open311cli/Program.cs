using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using static System.Console;

namespace Open311cli;

class Program
{
    const string requests_uri = "http://311.austintexas.gov/open311/v2/requests.json";
    const string services_uri = "http://311.austintexas.gov/open311/v2/services.json";
    
    static async Task Main()
    {
        WriteLine("Connecting to the 311 API...\n");
        await GetServices(services_uri);
        WriteLine("");
        await GetRequests(requests_uri);
    }

    private static async Task GetServices(string uri)
    {
        var response = await (new HttpClient()).GetAsync(uri);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<Open311Service>>(result);

            WriteLine($"{"service_code",24} {"service_name",50} {"group",35}");
            foreach (var item in items)
            {
                WriteLine($"{item.service_code, 24} {item.service_name, 50} {item.group, 35}");
            }
        }
    }

    private static async Task GetRequests(string uri)
    {
        var response = await (new HttpClient()).GetAsync(uri);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var result = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<Open311Item>>(result);

            WriteLine($"{"request_id",11} {"status",7} {"agency_responsible",28} {"requested_datetime",24}");
            foreach (var item in items)
            {
                WriteLine($"{item.service_request_id, 11} {item.status, 7} {item.agency_responsible, 28} {item.GetRequestedDatetime(), 24}");
            }
        }
    }
}