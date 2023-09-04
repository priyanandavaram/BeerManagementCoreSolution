using BeerManagement.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerManagement.Web.Services.Test.Common
{
    public class AppDbContextFixture : IDisposable
    {
        public DbContextOptions<AppDbContext> Options { get; private set; }

        private AppDbContext _dbContext;

        public AppDbContextFixture()
        {
            Options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

            _dbContext = new AppDbContext(Options);

            SeedData();
        }

        private void SeedData()
        {
            _dbContext.Beers.Add(new Beers { BeerId = 1, BeerName = "Kingfisher", PercentageAlcoholByVolume = 12.08M });
            _dbContext.Beers.Add(new Beers { BeerId = 2, BeerName = "Stella", PercentageAlcoholByVolume = 42.9M });
            _dbContext.Beers.Add(new Beers { BeerId = 3, BeerName = "Corona", PercentageAlcoholByVolume = 0.11M });
            _dbContext.Beers.Add(new Beers { BeerId = 4, BeerName = "Budweiser", PercentageAlcoholByVolume = 10.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 5, BeerName = "Peroni", PercentageAlcoholByVolume = 36.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 6, BeerName = "Peroni - Gold", PercentageAlcoholByVolume = 12.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 7, BeerName = "Peroni - Normal", PercentageAlcoholByVolume = 3.34M });
            _dbContext.Beers.Add(new Beers { BeerId = 8, BeerName = "Sparkling Water", PercentageAlcoholByVolume = 5.9M });
            _dbContext.Beers.Add(new Beers { BeerId = 9, BeerName = "Shine", PercentageAlcoholByVolume = 16.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 10, BeerName = "test", PercentageAlcoholByVolume = 26.8M });
            _dbContext.SaveChanges();

            _dbContext.Brewery.Add(new Brewery { BreweryId = 1, BreweryName = "United Beverages-Mexico" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 2, BreweryName = "United Beverages-Dallas" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 3, BreweryName = "United Beverages-London" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 4, BreweryName = "United Beverages-Leeds" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 5, BreweryName = "United Beverages-GatesHead" });
            _dbContext.SaveChanges();

            _dbContext.Bars.Add(new Bars { BarId = 1, BarName = "London Bar & Pub", BarAddress = "London Kings Cross" });
            _dbContext.Bars.Add(new Bars { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds City Center" });
            _dbContext.Bars.Add(new Bars { BarId = 3, BarName = "Metropolitan Bar", BarAddress = "Manchester High Street 05" });
            _dbContext.Bars.Add(new Bars { BarId = 4, BarName = "Metropolitan Bar & Pub", BarAddress = "Manchester High Street 06" });
            _dbContext.Bars.Add(new Bars { BarId = 5, BarName = "United club Bar", BarAddress = "Reading City Center" });
            _dbContext.SaveChanges();

            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 3, BeerId = 4 });
            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 3, BeerId = 1 });
            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 3, BeerId = 2 });
            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 4, BeerId = 1 });
            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 4, BeerId = 2 });
            _dbContext.LinkBreweryWithBeer.Add(new LinkBreweryWithBeer { BreweryId = 2, BeerId = 1 });
            _dbContext.SaveChanges();

            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 3, BeerId = 4 });
            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 3, BeerId = 1 });
            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 3, BeerId = 2 });
            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 4, BeerId = 1 });
            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 4, BeerId = 2 });
            _dbContext.LinkBarWithBeer.Add(new LinkBarWithBeer { BarId = 2, BeerId = 1 });
            _dbContext.SaveChanges();

        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
