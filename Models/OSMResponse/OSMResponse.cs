using System.Text.Json.Serialization;

namespace MyParking.Models;

public class OSMResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("bounds")]
    public Bounds Bounds { get; set; }

    [JsonPropertyName("nodes")]
    public List<long> Nodes { get; set; }

    [JsonPropertyName("geometry")]
    public List<Geometry> Geometry { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }
}