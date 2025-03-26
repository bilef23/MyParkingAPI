using MongoDB.Driver;
using MyParking.Data;
using MyParking.Helpers;
using MyParking.Models;
using Newtonsoft.Json;

namespace MyParking.Services.Impl;

public class ParkingService : IParkingService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApplicationDbContext _context;
    private readonly ILocationService _locationService;

    public ParkingService(IHttpClientFactory httpClientFactory, ApplicationDbContext context, ILocationService locationService)
    {
        _httpClientFactory = httpClientFactory;
        _context = context;
        _locationService = locationService;
    }

    public async Task<List<OSMResponse>> FetchParkingDataAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient();
        
        string overpassUrl = "https://overpass-api.de/api/interpreter";
        string query = @"
            [out:json];
            area[name=""Скопје""]->.searchArea;
            (
              node[""amenity""=""parking""](area.searchArea);
              way[""amenity""=""parking""](area.searchArea);
              relation[""amenity""=""parking""](area.searchArea);
            );
            (._;>;);
            out geom;";
        
        var content = new StringContent($"data={query}", System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        HttpResponseMessage response = await httpClient.PostAsync(overpassUrl, content);

        response.EnsureSuccessStatusCode();
        
        string jsonResponse = await response.Content.ReadAsStringAsync();
        var overpassResponse = JsonConvert.DeserializeObject<OSMRoot>(jsonResponse);
        
        return overpassResponse.Elements.Where(e => e.Type == "way").ToList();
    }

    public async Task<List<Parking>> FilterParkingDataByCoordinates(List<Parking> parkings)
    {
        var zones = await _context.Parkings.Find(_ => true).ToListAsync();
        
        return parkings
            .Where(p => p.Coordinates.Count>0)
            .ToList();
    }

    public async Task<List<Parking>> FilterParkingDataByZoneAndPrice(List<Parking> parkings)
    {
        return parkings.Where(p => p is not { ZoneId: null, Price: < 0 }).ToList();
    }

    public async Task SetParkingZone(Parking parking)
    {
        var zones = _context.Zones.Find(_ => true).ToList();
        double lat = parking.Coordinates.First()[0];
        double lon = parking.Coordinates.First()[1];

        var address =await  _locationService.GetAddressFromCoordinatesAsync(lat, lon);
        var matchingZone = zones.FirstOrDefault(zone => address.Any(addr => CyrillicToLatinConverter.CyrillicToLatin(addr).Equals(zone.Location, StringComparison.OrdinalIgnoreCase)));

        if (matchingZone != null)
        {
            parking.ZoneId = matchingZone.Id;
            parking.Zone = matchingZone;
        }
    }
    public async Task<List<Parking>> GetAllParkingsFromOSM()
    {
        var response = await FetchParkingDataAsync();
        var parsedResponse = ParkingParser.ParseFromOSMResponseElementsToParkingEntities(response);
        var parkingPlacesWithCoordinates = await FilterParkingDataByCoordinates(parsedResponse);
        
        foreach (var parking in parkingPlacesWithCoordinates)
        {
            if (parking.Price != 0)
            {
                await SetParkingZone(parking);
            }
        }
        
        var filteredParkingPlaces = await FilterParkingDataByZoneAndPrice(parkingPlacesWithCoordinates);
        
        return filteredParkingPlaces;
    }
    public async Task UpdateOrInsertParkingAsync(Parking newParking)
    {
        var filter = Builders<Parking>.Filter.Eq(p => p.ExternalId, newParking.ExternalId);

        var update = Builders<Parking>.Update
            .Set(p => p.Name, newParking.Name)
            .Set(p => p.HasChargingFee, newParking.HasChargingFee)
            .Set(p => p.Price, newParking.Price)
            .Set(p => p.Coordinates, newParking.Coordinates)
            .Set(p => p.ZoneId, newParking.ZoneId)
            .Set(p => p.Zone, newParking.Zone);

        // Use upsert (update or insert)
        var result = await _context.Parkings.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }

    public async Task<List<Parking>> GetParkingPlacesWithNoFee()
    {
        return await _context.Parkings.Find(p => p.Price == 0).ToListAsync();
    }

    public Parking GetParkingById(Guid Id)
    {
        return  _context.Parkings.Find(p => p.Id.Equals(Id)).FirstOrDefault();
    }

    public async Task InsertManyParkings(List<Parking> parkings)
    {
        await _context.Parkings.InsertManyAsync(parkings);
    }

    public Task<List<Parking>> GetAllParkingsFromDatabase()
    {
        return _context.Parkings.Find(_ => true).ToListAsync();
    }
}