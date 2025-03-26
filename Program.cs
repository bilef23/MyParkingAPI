using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MyParking.Data;
using MyParking.Jobs;
using MyParking.Models;
using MyParking.Services;
using MyParking.Services.Impl;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.Configure<ParkingDatabaseSettings>(
    builder.Configuration.GetSection("MongoDB"));
BsonSerializer.RegisterSerializer(typeof(Guid), new MongoDB.Bson.Serialization.Serializers.GuidSerializer(GuidRepresentation.Standard));

//Configure MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ParkingDatabaseSettings>>().Value;
    if (string.IsNullOrEmpty(options.ConnectionString))
    {
        throw new InvalidOperationException("MongoConnection is not configured.");
    }
    return new MongoClient(options.ConnectionString);
});

builder.Services.AddSingleton<ApplicationDbContext>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var options = sp.GetRequiredService<IOptions<ParkingDatabaseSettings>>().Value;
    return new ApplicationDbContext(client, options);
});
//Add Services
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IParkingService, ParkingService>();
builder.Services.AddScoped<IZoneService, ZoneService>();

//Configure Job
builder.Services.AddQuartz();
builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});
builder.Services.ConfigureOptions<InsertUpdateParkingJobSetup>();

builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        
        await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
    }
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); 


app.Run();
