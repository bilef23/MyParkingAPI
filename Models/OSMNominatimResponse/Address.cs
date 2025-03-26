using System.Text.Json.Serialization;

namespace MyParking.Models;

public class Address
{
    [JsonPropertyName("road")]
    public string Road { get; set; }
    
    [JsonPropertyName("city")]
    public string City { get; set; }
}