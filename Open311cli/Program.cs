using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Console;

namespace Open311cli;

class Program
{
    const string requests_uri = "http://311.austintexas.gov/open311/v2/requests.json";
    const string services_uri = "http://311.austintexas.gov/open311/v2/services.json";
    
    static async Task Main()
    {
        WriteLine("Connecting to the 311 API...");
        await GetServices();
        await GetRequests();
    }

    private static async Task<string> GetResponseAsync(string uri)
    {
        var response = await(new HttpClient()).GetAsync(uri);
        if (response.StatusCode == HttpStatusCode.OK)
            return await response.Content.ReadAsStringAsync();
        else
            return "[]";
    }

    private static async Task GetServices()
    {
        var result = await GetResponseAsync(services_uri);
        var items = Open311Service.FromJson(result);

        WriteLine($"\n{"service_code", 24} {"service_name", 50} {"group", 35}");
        foreach (var item in items)
        {
            WriteLine($"{item.service_code, 24} {item.service_name, 50} {item.group, 35}");
        }
    }

    private static async Task GetRequests()
    {
        var result = await GetResponseAsync(requests_uri);
        var items = Open311Item.FromJson(result);

        WriteLine($"\n{"request_id", 11} {"status", 7} {"agency_responsible", 50} {"requested_datetime", 24}");
        foreach (var item in items)
        {
            WriteLine($"{item.service_request_id, 11} {item.status, 7} {item.agency_responsible, 50} {item.GetRequestedDatetime(), 24}");
        }
    }
}