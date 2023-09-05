using BeerManagement.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
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
            _dbContext.Beers.Add(new Beers { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M });
            _dbContext.Beers.Add(new Beers { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M });
            _dbContext.Beers.Add(new Beers { BeerId = 3, BeerName = "Michelob ULTRA", PercentageAlcoholByVolume = 0.11M });
            _dbContext.Beers.Add(new Beers { BeerId = 4, BeerName = "Modelo Especial", PercentageAlcoholByVolume = 10.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 5, BeerName = "Stella Artois", PercentageAlcoholByVolume = 36.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 6, BeerName = "Lagunitas IPA", PercentageAlcoholByVolume = 12.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 7, BeerName = "Corona Light", PercentageAlcoholByVolume = 3.34M });
            _dbContext.Beers.Add(new Beers { BeerId = 8, BeerName = "Budweiser", PercentageAlcoholByVolume = 5.9M });
            _dbContext.Beers.Add(new Beers { BeerId = 9, BeerName = "New Belgium Voodoo Ranger Imperial IPA", PercentageAlcoholByVolume = 16.1M });
            _dbContext.Beers.Add(new Beers { BeerId = 10, BeerName = "Yuengling Traditional Lager", PercentageAlcoholByVolume = 26.8M });
            _dbContext.SaveChanges();
            _dbContext.Brewery.Add(new Brewery { BreweryId = 1, BreweryName = "Carlsberg Group" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 2, BreweryName = "Sierra Nevada Brewing Co." });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 3, BreweryName = "Bell's Brewery Inc." });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 4, BreweryName = "New Belgium Brewing Company" });
            _dbContext.Brewery.Add(new Brewery { BreweryId = 5, BreweryName = "Flying Dog" });
            _dbContext.SaveChanges();
            _dbContext.Bars.Add(new Bars { BarId = 1, BarName = "Red Lion", BarAddress = "London Kings Cross" });
            _dbContext.Bars.Add(new Bars { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds High street" });
            _dbContext.Bars.Add(new Bars { BarId = 3, BarName = "The Plough", BarAddress = "Manchester High Street 05" });
            _dbContext.Bars.Add(new Bars { BarId = 4, BarName = "Rose & Crown", BarAddress = "Kensington, London" });
            _dbContext.Bars.Add(new Bars { BarId = 5, BarName = "Chequers", BarAddress = "06 Bath Rd, Slough" });
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