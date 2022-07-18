using System.Collections.Generic;
using System.Text.Json;

namespace Open311cli;

public class Open311Service
{
    public string service_code { get; set; }
    public string service_name { get; set; }
    public string description { get; set; }
    public bool metadata { get; set; }
    public string type { get; set; }
    public string group { get; set; }

    public static List<Open311Service> FromJson(string json)
    {
        return JsonSerializer.Deserialize<List<Open311Service>>(json);
    }
}