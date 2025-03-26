using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyParking.Models;

public class Parking
{
    [BsonId]
    public Guid Id { get; set; }
    
    public string ExternalId { get; set; }
    
    public string Name { get; set; }
    
    public int Price { get; set; } = 0;
    
    public string HasChargingFee { get; set; }
    
    public Guid? ZoneId { get; set; }
    
    public Zone? Zone { get; set; }
    public List<List<double>> Coordinates { get; set; }
}