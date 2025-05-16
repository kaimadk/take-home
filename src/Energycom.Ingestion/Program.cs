using System.Runtime.CompilerServices;
using Energycom.Ingestion;
using Energycom.Ingestion.Data;
using Energycom.Ingestion.Entities;
using Energycom.Ingestion.Ingest;
using Energycom.Ingestion.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddOpenApi();



builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

builder.AddNpgsqlDbContext<ECOMDbContext>("EnergycomDb", settings =>
{
    settings.DisableRetry = true;
    settings.CommandTimeout = 30;

}, options =>
{
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
    options.UseSnakeCaseNamingConvention();
});

//This is a dumb way to do this, but is fine for this limited use case
#pragma warning disable ASP0000
await MigrateAndSeed.Execute(builder.Services.BuildServiceProvider());
#pragma warning restore ASP0000


builder.Services.AddHostedService<TimedHostedService>();
builder.Services.AddSingleton<NotificationHub>();

var app = builder.Build();
app.MapDefaultEndpoints();
app.MapOpenApi();
app.MapScalarApiReference($"/scalar", options =>
{
    options
        .WithTitle("Energycom Ingestion")
        .WithTheme(ScalarTheme.Kepler)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);


    //hack for aspire url passthrough
    options.Servers = [];
});

app.UseHttpsRedirection();


app.MapGet("/readings/stream", 
        ( [FromServices] NotificationHub hub, CancellationToken token,  [FromServices] ILogger<Program> logger) 
        => GetDataWithCancellationAsync(hub,logger, token ))
    .WithName("GetReadingsStream");

app.MapGet("/readings/{from:datetime}/{to:datetime}", async (DateTime from, DateTime to, [FromServices] ECOMDbContext context ) =>
{
    var readings = await context.Readings
        .Include(reading => reading.Meter)
        .ThenInclude(meter => meter.Group)
        .Where(e => e.ReadingDate >= from && e.ReadingDate < to)
        .ToListAsync();

    return Results.Ok(readings.Select(MapToReadingModel).ToList());
}).WithName("GetReadings").WithTags("Get readings between from(inclusive) and to(exclusive)");




app.Run();

async IAsyncEnumerable<ReadingModelV1> GetDataWithCancellationAsync(NotificationHub hub, ILogger logger,  [EnumeratorCancellation] CancellationToken cancellationToken = default)
{
    var channel = await hub.Subscribe();
    while (!cancellationToken.IsCancellationRequested)
    { 
        await foreach (var reading in channel.Reader.ReadAllAsync(cancellationToken))
        {
            
            var model = MapToReadingModel(reading);
            yield return model;
        }
    }

    logger.LogInformation("Cancellation requested, stopping stream.");
    await hub.Unsubscribe(channel);
}

static ReadingModelV1 MapToReadingModel(Reading reading)
{
    return new ReadingModelV1(reading.Id, reading.RawJson, reading.Meter.MeterNumber, reading.ReadingDate, reading.Meter.Group.Id, reading.Unit, reading.Value);
}