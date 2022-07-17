using System;
using System.Text.Json.Serialization;

namespace Open311cli;

public class Open311Item
{
    public string service_request_id { get; set; }
    public string status { get; set; }
    public string service_name { get; set; }
    public string service_code { get; set; }
    public string agency_responsible { get; set; }
    public string requested_datetime { get; set; }
    public string updated_datetime { get; set; }
    public string address { get; set; }
    public float lat { get; set; }

    [JsonPropertyName("long")]
    public float lng { get; set; }

    public DateTime GetRequestedDatetime()
    {
        if (DateTime.TryParse(requested_datetime, out var dt)) 
            return dt; 
        else 
            return DateTime.MinValue; 
    }
}