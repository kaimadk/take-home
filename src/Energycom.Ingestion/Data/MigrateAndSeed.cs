using Energycom.Ingestion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Energycom.Ingestion.Data;

public static class MigrateAndSeed
{
    
    public static async Task Execute(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ECOMDbContext>();
      
        await context.Database.MigrateAsync();
        
        //Seed data for the the take home
        
        // - Group
        //     - Meter
        //         - Reading
        //
        
        //check if groups exist
        if (await context.Groups.AnyAsync())
        {
            return;
        }
        
        //Creating two groups 
        
        var groups = new List<Group>();
        
        var group1 = new Group
        {
            Id = 1,
            Name = "Group 1",
            Meters = []
        };
        
        var group2 = new Group
        {
            Id = 2,
            Name = "Group 2",
            Meters = []
        };
        
        var group3 = new Group
        {
            Id = 3,
            Name = "Group 3",
            Meters = []
        };
        
        var group4 = new Group
        {
            Id = 4,
            Name = "Group 4",
            Meters = []
        };
        
        groups.Add(group1);
        groups.Add(group2);
        groups.Add(group3);
        groups.Add(group4);


        int meterId = 0;
        foreach (var group in groups)
        {
            
            //create a random number of meters between 13 and 20
            
            var numberOfMeters = Random.Shared.Next(13, 21);
            
            for (var i = 0; i < numberOfMeters; i++)
            {
                var meter = new Meter
                {
                    Id = ++meterId,
                    GroupId = group.Id,
                    MeterNumber = $"C{meterId.ToString().PadLeft(5, '0')}",
                    Readings = [],
                    Configuration = new MeterConfiguration
                    {
                        Id = meterId,
                        Type = MeterType.Consumption,
                        BaseValue = Random.Shared.Next(50, 201),
                        CanSkipReadings = false,
                        CanDuplicateReadings = false,
                        CanHaveUnparsedReadings = false
                    }
                };
                
                group.Meters.Add(meter);
            }
            
        }
        
        
      

        foreach (var group in groups)
        {
            
            int numberSkippingMetersPerGroup = Random.Shared.Next(1, 4);
            int numberDuplicatingMeters = Random.Shared.Next(1, 4);
            int numberOfProductionMeters = Random.Shared.Next(1, 6);
            int numberFaultyMeters = 0;
            
            //select ids for skipping  and duplicating meters
            
            var metersToConfigure = group.Meters
                .OrderBy(x => Guid.NewGuid())
                .Take(numberSkippingMetersPerGroup + numberDuplicatingMeters + numberFaultyMeters)
                .ToList();
            
            //configure meters to skip readings
            for(var i = 0; i < numberSkippingMetersPerGroup; i++)
            {
                var meter = metersToConfigure[i];
                meter.Configuration.CanSkipReadings = true;
            }
            
            //configure meters to duplicate readings
            for(var i = numberSkippingMetersPerGroup; i < numberDuplicatingMeters + numberSkippingMetersPerGroup; i++)
            {
                var meter = metersToConfigure[i];
                meter.Configuration.CanDuplicateReadings = true;
            }
            
            //configure meters to have unparsed readings
            for(var i = numberDuplicatingMeters + numberSkippingMetersPerGroup; i < numberDuplicatingMeters + numberSkippingMetersPerGroup + numberFaultyMeters; i++)
            {
                var meter = metersToConfigure[i];
                meter.Configuration.CanHaveUnparsedReadings = true;
            }


            for (var i = 0; i < numberOfProductionMeters; i++)
            {
                var meter = new Meter
                {
                    Id = ++meterId,
                    GroupId = group.Id,
                    MeterNumber = $"P{meterId.ToString().PadLeft(5, '0')}",
                    Readings = [],
                    Configuration = new MeterConfiguration
                    {
                        Id = meterId,
                        Type = MeterType.Consumption,
                        BaseValue = -1* Random.Shared.Next(50, 201),
                        CanSkipReadings = false,
                        CanDuplicateReadings = false,
                        CanHaveUnparsedReadings = false
                    }
                };
                
                group.Meters.Add(meter);
            }
            
        }
        context.Groups.AddRange(groups);        
        await context.SaveChangesAsync();
    }
    
    
}