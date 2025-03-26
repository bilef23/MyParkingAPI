using MyParking.Data;
using MyParking.Models;

namespace MyParking.Services.Impl;

public class ZoneService : IZoneService
{
    private ApplicationDbContext _context;

    public ZoneService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InserManyZones(List<Zone> zones)
    {
        await _context.Zones.InsertManyAsync(zones);
    }
}