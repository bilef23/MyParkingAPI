using MyParking.Models;

namespace MyParking.Services;

public interface IParkingService
{
    Task<List<OSMResponse>> FetchParkingDataAsync();
    Task<List<Parking>> FilterParkingDataByCoordinates(List<Parking> parkings);
    Task<List<Parking>> FilterParkingDataByZoneAndPrice(List<Parking> parkings);
    Task<List<Parking>> GetAllParkingsFromOSM();
    Task InsertManyParkings(List<Parking> parkings);
    Task<List<Parking>> GetAllParkingsFromDatabase();
    Task UpdateOrInsertParkingAsync(Parking newParking);
    Task<List<Parking>> GetParkingPlacesWithNoFee();
    Parking GetParkingById(Guid Id);
}