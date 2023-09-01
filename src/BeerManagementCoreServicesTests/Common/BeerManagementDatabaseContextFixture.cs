using BeerManagementCoreServices.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeerManagementCoreServicesTests.Common
{
    public class BeerManagementDatabaseContextFixture : IDisposable
    {
        public DbContextOptions<BeerManagementDatabaseContext> Options { get; private set; }

        private BeerManagementDatabaseContext _bmsContext;

        public BeerManagementDatabaseContextFixture()
        {
            Options = new DbContextOptionsBuilder<BeerManagementDatabaseContext>()
                        .UseInMemoryDatabase(databaseName: "BMSDatabase_Test")
                        .Options;

            _bmsContext = new BeerManagementDatabaseContext(Options);
            SeedData();
        }

        private void SeedData()
        {
            _bmsContext.Beers.RemoveRange(_bmsContext.Beers);
            _bmsContext.SaveChanges();

            _bmsContext.Brewery.RemoveRange(_bmsContext.Brewery);
            _bmsContext.SaveChanges();

            _bmsContext.Bars.RemoveRange(_bmsContext.Bars);
            _bmsContext.SaveChanges();

            _bmsContext.Beers.Add(new Beers { BeerId = 1, BeerName = "Kingfisher", PercentageAlcoholByVolume = 12.08M });
            _bmsContext.Beers.Add(new Beers { BeerId = 2, BeerName = "Stella", PercentageAlcoholByVolume = 42.9M });
            _bmsContext.Beers.Add(new Beers { BeerId = 3, BeerName = "Corona", PercentageAlcoholByVolume = 0.11M });
            _bmsContext.Beers.Add(new Beers { BeerId = 4, BeerName = "Budweiser", PercentageAlcoholByVolume = 10.1M });
            _bmsContext.Beers.Add(new Beers { BeerId = 5, BeerName = "Peroni", PercentageAlcoholByVolume = 36.1M });
            _bmsContext.Beers.Add(new Beers { BeerId = 6, BeerName = "Peroni - Gold", PercentageAlcoholByVolume = 12.1M });
            _bmsContext.Beers.Add(new Beers { BeerId = 7, BeerName = "Peroni - Normal", PercentageAlcoholByVolume = 3.34M });
            _bmsContext.Beers.Add(new Beers { BeerId = 8, BeerName = "Sparkling Water", PercentageAlcoholByVolume = 5.9M });
            _bmsContext.Beers.Add(new Beers { BeerId = 9, BeerName = "Shine", PercentageAlcoholByVolume = 16.1M });
            _bmsContext.Beers.Add(new Beers { BeerId = 10, BeerName = "test", PercentageAlcoholByVolume = 26.8M });
            _bmsContext.SaveChanges();

            _bmsContext.Brewery.Add(new Brewery { BreweryId = 1, BreweryName = "United Beverages-Mexico" });
            _bmsContext.Brewery.Add(new Brewery { BreweryId = 2, BreweryName = "United Beverages-Dallas" });
            _bmsContext.Brewery.Add(new Brewery { BreweryId = 3, BreweryName = "United Beverages-London" });
            _bmsContext.Brewery.Add(new Brewery { BreweryId = 4, BreweryName = "United Beverages-Leeds" });
            _bmsContext.Brewery.Add(new Brewery { BreweryId = 5, BreweryName = "United Beverages-GatesHead" });            
            _bmsContext.SaveChanges();

            _bmsContext.Bars.Add(new Bars { BarId = 1, BarName = "London Bar & Pub", BarAddress = "London Kings Cross" });
            _bmsContext.Bars.Add(new Bars { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds City Center" });
            _bmsContext.Bars.Add(new Bars { BarId = 3, BarName = "Metropolitan Bar", BarAddress = "Manchester High Street 05" });
            _bmsContext.Bars.Add(new Bars { BarId = 4, BarName = "Metropolitan Bar & Pub", BarAddress = "Manchester High Street 06" });
            _bmsContext.Bars.Add(new Bars { BarId = 5, BarName = "United club Bar", BarAddress = "Reading" });
            _bmsContext.SaveChanges();
        }

        public void Dispose()
        {
            _bmsContext.Dispose();
        }
    }
}
