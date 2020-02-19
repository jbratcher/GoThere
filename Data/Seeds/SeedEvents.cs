using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GoThere.Models;
using System;
using System.Linq;


namespace GoThere.Data.Seeds
{
    public static class SeedEvents
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any events
                if (context.Events.Any())
                {
                    return;   // DB has been seeded
                }

                context.Events.AddRange(
                    new Event
                    {
                        Name = "New Event",
                        Description = "A lot of words show go here, full description",
                        Type = "Conference",
                        StartDateTime = DateTime.Now,
                        EndDateTime = DateTime.Now,
                        Price = 99.0m,
                        LocationName = "Location Name",
                        StreetAddress = "100 Main Street",
                        City = "Louisville",
                        State = "Kentucky",
                        Country = "USA",
                        PostalCode = "40202",
                    },
                    new Event
                    {
                        Name = "Old Event",
                        Description = "A lot of words show go here, full description",
                        Type = "Continuing Education",
                        StartDateTime = DateTime.Now,
                        EndDateTime = DateTime.Now,
                        Price = 99.0m,
                        LocationName = "Location Name",
                        StreetAddress = "100 Main Street",
                        City = "Denver",
                        State = "Colorado",
                        Country = "USA",
                        PostalCode = "80212",
                    },
                    new Event
                    {
                        Name = "New Event",
                        Description = "A lot of words show go here, full description",
                        Type = "Conference",
                        StartDateTime = DateTime.Now,
                        EndDateTime = DateTime.Now,
                        Price = 99.0m,
                        LocationName = "Location Name",
                        StreetAddress = "100 Main Street",
                        City = "New York",
                        State = "New York",
                        Country = "USA",
                        PostalCode = "10001",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}