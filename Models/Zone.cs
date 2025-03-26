using MongoDB.Bson.Serialization.Attributes;

namespace MyParking.Models;

public class Zone
{
    public Guid Id { get; set; }
    
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public ZoneType ZoneType { get; set; }
    public int FirstHourPrice { get; set; }
    public int TimeLimit { get; set; }
    public int NumberOfParkingPlaces { get; set; }
    public int AdditionalHourPrice { get; set; }
    public string Location { get; set; }
    [BsonIgnore]
    public List<Parking> Parkings { get; set; }
}