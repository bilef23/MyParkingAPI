using MyParking.Models;
using MyParking.Services;
using MyParking.Services.Impl;

namespace MyParking.Jobs;

using Quartz;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class InsertUpdateParkingJob : IJob
{
    private readonly IParkingService _parkingService;
    public const string Key = "InsertUpdateParking";
    
    public InsertUpdateParkingJob(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var updatedParkingData = await _parkingService.GetAllParkingsFromOSM();

        foreach (var parking in updatedParkingData)
        {
            await _parkingService.UpdateOrInsertParkingAsync(parking);
        }
    }
    
}
