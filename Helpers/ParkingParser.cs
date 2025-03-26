using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using MyParking.Models;

namespace MyParking.Helpers;

public static class ParkingParser
{
        public static List<Parking> ParseFromOSMResponseElementsToParkingEntities(List<OSMResponse> responses)
        {
            return responses
                .Where(osm => osm.Tags != null)
                .Select(osm =>
                {
                    string name = GetTagValue(osm, "name", "Unnamed Parking");
                    string hasChargingFee = GetTagValue(osm, "fee", "Unknown");
                    int price = GetPrice(osm);
                    
                    var coordinates = osm.Geometry?.Where(g => g.Lat != 0 && g.Lon != 0)
                        .Select(g => new List<double> { g.Lat, g.Lon })
                        .ToList() ?? new List<List<double>>(); 
                    
                    return new Parking
                    {
                        Id = Guid.NewGuid(),
                        ExternalId = osm.Id.ToString(),
                        Name = name,
                        HasChargingFee = hasChargingFee,
                        Price = price,
                        Coordinates = coordinates
                    };
                })
                .ToList();
        }

        // Helper method to determine the value of a tag
        private static string GetTagValue(OSMResponse osm, string key, string defaultValue = null)
        {
            return osm.Tags != null && osm.Tags.ContainsKey(key) ? osm.Tags[key] : defaultValue;
        }

        // Helper method to determine price based on charge and fee
        private static int GetPrice(OSMResponse osm)
        {
            if (osm.Tags.ContainsKey("fee") && osm.Tags["fee"].ToLower() == "no")
            {
                return 0;  // If fee is "no", set price to 0
            }

            if (osm.Tags.ContainsKey("charge"))
            {
                var match = Regex.Match(osm.Tags["charge"], @"\d+");
                if (match.Success)
                {
                    return int.Parse(match.Value);  // Extract price from charge
                }
            }

            return -1;  // Default value if no price available
        }
}