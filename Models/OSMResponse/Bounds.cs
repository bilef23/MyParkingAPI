using System.Text.Json.Serialization;

namespace MyParking.Models;

public class Bounds
{
    [JsonPropertyName("minlat")]
    public double MinLat { get; set; }

    [JsonPropertyName("minlon")]
    public double MinLon { get; set; }

    [JsonPropertyName("maxlat")]
    public double MaxLat { get; set; }

    [JsonPropertyName("maxlon")]
    public double MaxLon { get; set; }
}