using Microsoft.AspNetCore.Mvc;
using MyParking.Data;
using MyParking.Services;

namespace MyParking.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;
        public ParkingController(IParkingService parkingService,ApplicationDbContext context, ILocationService locationService)
        {
            _context = context;
            _locationService = locationService;
            _parkingService = parkingService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllParkingPlaces()
        {
            var parkingPlaces =await  _parkingService.GetAllParkingsFromDatabase();

            return parkingPlaces.Any() ? Ok(parkingPlaces) : NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetParkingPlacesWithNoFee()
        {
            var parkingPlaces =await  _parkingService.GetParkingPlacesWithNoFee();

            return parkingPlaces.Any() ? Ok(parkingPlaces) : NoContent();
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetParkingById(Guid Id)
        {
            var parking = _parkingService.GetParkingById(Id);

            return parking != null ? Ok(parking) : NoContent();
        }
    }
}
