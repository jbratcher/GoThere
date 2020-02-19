using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GoThere.Models;
using System;
using System.Linq;


namespace GoThere.Data.Seeds
{
    public static class SeedLocations
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Locations.Any())
                {
                    return;   // DB has been seeded
                }

                context.Locations.AddRange(
                    new Location
                    {
                        Name = "Space Needle",
                        StreetAddress = "400 Broad St",
                        City = "Seattle",
                        State = "Washington",
                        Country = "United States",
                        PostalCode = "98109"
                    },
                    new Location
                    {
                        Name = "Old Town San Diego State Historic Park",
                        StreetAddress = "4002 Wallace St",
                        City = "San Diego",
                        State = "California",
                        Country = "United States",
                        PostalCode = "92110"
                    },
                    new Location
                    {
                        Name = "Empire State Building",
                        StreetAddress = "20 W 34th St",
                        City = "New York City",
                        State = "New York",
                        Country = "United States",
                        PostalCode = "10001"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}