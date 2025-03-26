using System.Text.Json.Serialization;

namespace MyParking.Models;

public class Geometry
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}