using MyParking.Models;

namespace MyParking.Services;

public interface IZoneService
{
    Task InserManyZones(List<Zone> zones);
}