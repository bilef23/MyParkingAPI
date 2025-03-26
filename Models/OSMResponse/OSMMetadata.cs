using Newtonsoft.Json;

namespace MyParking.Models;

public class OSMMetadata
{
    [JsonProperty("timestamp_osm_base")]
    public string TimestampOsmBase { get; set; }
}