using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using MyParking.Models;

namespace MyParking.Data;

public class ApplicationDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<Parking> Parkings => _database.GetCollection<Parking>("Parking");
    public IMongoCollection<Zone> Zones => _database.GetCollection<Zone>("Zone");
    public ApplicationDbContext(IMongoClient mongoClient, ParkingDatabaseSettings options)
    {
        var connectionString = options.ConnectionString;
            
        var mongoUrl = new MongoUrl(connectionString);
        _database = mongoClient.GetDatabase(options.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}