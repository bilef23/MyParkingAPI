using Microsoft.Extensions.Options;
using Quartz;

namespace MyParking.Jobs;

public class InsertUpdateParkingJobSetup :IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = InsertUpdateParkingJob.Key;

        options.AddJob<InsertUpdateParkingJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger.ForJob(jobKey)
                    .WithCronSchedule("0 0 0 * * ?"));
    }
}