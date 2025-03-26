namespace MyParking.Services;

public interface ILocationService
{
    Task<List<string>> GetAddressFromCoordinatesAsync(double latitude, double longitude);
}