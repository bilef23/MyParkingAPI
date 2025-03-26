using MongoDB.Driver;
using MyParking.Models;
using MyParking.Services;

namespace MyParking.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var parkingService = scope.ServiceProvider.GetRequiredService<IParkingService>();
            var zoneService = scope.ServiceProvider.GetRequiredService<IZoneService>();
            
            await SeedZonesAsync(context,zoneService);
            await SeedParkingAsync(parkingService, context);
        }
    }

    private static async Task SeedParkingAsync(IParkingService _parkingService,ApplicationDbContext context)
    {
        if (!context.Parkings.AsQueryable().Any())
        {
            var parkings = await _parkingService.GetAllParkingsFromOSM();
            await _parkingService.InsertManyParkings(parkings);
        }
        
    }
    private static async Task SeedZonesAsync(ApplicationDbContext context,IZoneService _zoneService)
    {
        if (!context.Zones.AsQueryable().Any())
        {
            var zones = new List<Zone>
            {
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A0, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Parking lot - Sredno Vodno", NumberOfParkingPlaces = 154 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A01, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Blaze Koneski", NumberOfParkingPlaces = 42 },
                //new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A02, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimitInHours = 0, Location = "Parking lot - Pantelejmon", NumberOfParkingPlaces = 39 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A05, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Orce Nikolov", NumberOfParkingPlaces = 39 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A06, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Maksim Gorki", NumberOfParkingPlaces = 24 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A3, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Dame Gruev", NumberOfParkingPlaces = 53 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A4, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimit = 0, Location = "Dame Gruev", NumberOfParkingPlaces = 85 },
                //new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.A8, FirstHourPrice = 75, AdditionalHourPrice = 50, TimeLimitInHours = 0, Location = "Smilevski Kongres, ", NumberOfParkingPlaces = 151 }, Problem with this Zone due to the streets
                
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.B2, FirstHourPrice = 40, AdditionalHourPrice = 30, TimeLimit = 4, Location = "Mito Hadživasilev Jasmin", NumberOfParkingPlaces = 57 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.B3, FirstHourPrice = 40, AdditionalHourPrice = 30, TimeLimit = 4, Location = "Sv. Kiril i Metodij", NumberOfParkingPlaces = 49 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.B6, FirstHourPrice = 40, AdditionalHourPrice = 30, TimeLimit = 4, Location = "GJuro Strugar", NumberOfParkingPlaces = 26 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.B7, FirstHourPrice = 40, AdditionalHourPrice = 30, TimeLimit = 4, Location = "Stiv Naumov", NumberOfParkingPlaces = 77 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.B10, FirstHourPrice = 40, AdditionalHourPrice = 30, TimeLimit = 4, Location = "Lazar Ličenoski", NumberOfParkingPlaces = 24 },

                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C8, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Kosta Shahov", NumberOfParkingPlaces = 7 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C9, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Debarca", NumberOfParkingPlaces = 22 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C15, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Miroslav Krleza", NumberOfParkingPlaces = 73 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C17, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Leninova", NumberOfParkingPlaces = 22 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C19, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "29-ti Noemvri", NumberOfParkingPlaces = 26 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C33, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Nikola Karev", NumberOfParkingPlaces = 136 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C35, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Stale Popov", NumberOfParkingPlaces = 53 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C45, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Bulevar Goce Delčev", NumberOfParkingPlaces = 380 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C46, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Ново Маало", NumberOfParkingPlaces = 193 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C80, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Boulevard Ilinden", NumberOfParkingPlaces = 99 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.C81, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Bulevar Ilinden", NumberOfParkingPlaces = 131 },

                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D1, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Bulevar ASNOM", NumberOfParkingPlaces = 31 },
                //new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D2, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimitInHours = 0, Location = "Tri Biseri, Municipality Aerodrom", NumberOfParkingPlaces = 780 }, OSM doesnt show the parking nodes near Tri Biseri
                //new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D3, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimitInHours = 0, Location = "Behind Biser mall", NumberOfParkingPlaces = 1652 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D4, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Near the supermarket Vero, Municipality Aerodrom", NumberOfParkingPlaces = 727 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D5, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Vladimir Komarov", NumberOfParkingPlaces = 116 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D6, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Vladimir Komarov", NumberOfParkingPlaces = 154 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D7, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Јане Сандански", NumberOfParkingPlaces = 660 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D8, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Bojmija", NumberOfParkingPlaces = 63 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D9, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "23 Октомври", NumberOfParkingPlaces = 410 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D40, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Hospital 8th of September", NumberOfParkingPlaces = 65 },
                new Zone { Id = Guid.NewGuid(), ZoneType = ZoneType.D62, FirstHourPrice = 30, AdditionalHourPrice = 25, TimeLimit = 0, Location = "Tennis-court ABC", NumberOfParkingPlaces = 33 }
            };

            await _zoneService.InserManyZones(zones);
        }
    }
}