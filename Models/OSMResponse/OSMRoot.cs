using Newtonsoft.Json;

namespace MyParking.Models;

public class OSMRoot
{
    [JsonProperty("version")]
    public double Version { get; set; }

    [JsonProperty("generator")]
    public string Generator { get; set; }

    [JsonProperty("osm3s")]
    public OSMMetadata OSM3S { get; set; }

    [JsonProperty("elements")]
    public List<OSMResponse> Elements { get; set; }
}