using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyParking.Models;

public class NominatimResponse
{
    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
    
    [JsonProperty("address")]
    public Address Address { get; set; }
}