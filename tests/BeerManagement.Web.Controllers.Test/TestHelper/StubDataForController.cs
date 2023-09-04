using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Web.Controllers.Test.TestHelper
{
    public static class StubDataForController
    {
        public static List<BarModel> GetBarDetails()
        {
            List<BarModel> barDetails = new List<BarModel>
            {
                new BarModel { BarId = 1, BarName = "London Bar & Pub", BarAddress = "London Kings Cross" },
                new BarModel { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds City Center" },
                new BarModel { BarId = 3, BarName = "Metropolitan Bar", BarAddress = "Manchester High Street 05" },
                new BarModel { BarId = 4, BarName = "Metropolitan Bar & Pub", BarAddress = "Manchester High Street 06" },
                new BarModel { BarId = 5, BarName = "United club Bar", BarAddress = "Reading" }
            };
            return barDetails;
        }
        public static List<BreweryModel> GetBreweryDetails()
        {
            List<BreweryModel> breweryDetails = new List<BreweryModel>
            {
                new BreweryModel { BreweryId = 1, BreweryName = "United Beverages-Mexico" },
                new BreweryModel { BreweryId = 2, BreweryName = "United Beverages-Dallas" },
                new BreweryModel { BreweryId = 3, BreweryName = "United Beverages-London" },
                new BreweryModel { BreweryId = 4, BreweryName = "United Beverages-Leeds" },
                new BreweryModel { BreweryId = 5, BreweryName = "United Beverages-GatesHead" }
            };
            return breweryDetails;
        }
        public static List<BeerModel> GetBeerDetails()
        {
            List<BeerModel> beerDetails = new List<BeerModel>
            {
                new BeerModel { BeerId = 1, BeerName = "Kingfisher", PercentageAlcoholByVolume = 12.08M },
                new BeerModel { BeerId = 2, BeerName = "Stella", PercentageAlcoholByVolume = 42.9M },
                new BeerModel { BeerId = 3, BeerName = "Corona", PercentageAlcoholByVolume = 0.11M },
                new BeerModel { BeerId = 4, BeerName = "Budweiser", PercentageAlcoholByVolume = 10.1M },
                new BeerModel { BeerId = 5, BeerName = "Peroni", PercentageAlcoholByVolume = 36.1M }
            };
            return beerDetails;
        }
        public static List<BreweryWithAssociatedBeersModel> GetBreweryAndBeerDetailsById()
        {
            var obj = new List<BreweryWithAssociatedBeersModel>
            {
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 1,
                        BreweryName = "United Beverages-Mexico",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "BeerName2", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 2, BeerName = "BeerName1", PercentageAlcoholByVolume = 0.5M }
                        }
                }
            };
            return obj;
        }
        public static List<BarsWithAssociatedBeersModel> GetBarsAndBeerDetailsById()
        {
            var obj = new List<BarsWithAssociatedBeersModel>
            {
                new BarsWithAssociatedBeersModel{
                        BarId = 4,
                        BarName = "Leeds Fish & Bar",
                        BarAddress = "Leeds",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Budweiser", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 2, BeerName = "Peroni", PercentageAlcoholByVolume = 0.5M }
                        }
                }
            };
            return obj;
        }
        public static List<BreweryWithAssociatedBeersModel> GetBreweryAndBeerDetails()
        {
            var obj = new List<BreweryWithAssociatedBeersModel>
            {
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 1,
                        BreweryName = "United Beverages-Mexico",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "BeerName2", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 2, BeerName = "BeerName1", PercentageAlcoholByVolume = 0.5M }
                        }
                },
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 2,
                        BreweryName = "United Beverages-Dallas",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "BeerName3", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 2, BeerName = "BeerName4", PercentageAlcoholByVolume = 0.5M }
                        }
                }
            };
            return obj;
        }
        public static List<BarsWithAssociatedBeersModel> GetBarsAndBeerDetails()
        {
            var obj = new List<BarsWithAssociatedBeersModel>
            {
                new BarsWithAssociatedBeersModel{
                        BarId = 4,
                        BarName = "Leeds Fish & Bar",
                        BarAddress = "Leeds",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Budweiser", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 2, BeerName = "Peroni", PercentageAlcoholByVolume = 0.5M  }
                        }
                },
                new BarsWithAssociatedBeersModel{
                        BarId = 5,
                        BarName = "Manchester Fish & Bar",
                        BarAddress = "Manchester",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 3, BeerName = "Beer1", PercentageAlcoholByVolume = 1.5M },
                              new BeerModel { BeerId = 4, BeerName = "Beer2", PercentageAlcoholByVolume = 0.5M  }
                        }
                }
            };
            return obj;
        }
        public static BreweryAndBeerModel BreweryAndBeerInfo()
        {
            BreweryAndBeerModel data = new BreweryAndBeerModel { BreweryId = 1, BeerId = 2 };
            return data;
        }
        public static BarAndBeerModel BarAndBeerInfo()
        {
            BarAndBeerModel data = new BarAndBeerModel { BarId = 1, BeerId = 2 };
            return data;
        }                  
        public static BreweryModel InitializeBreweryData(int breweryId, string breweryName)
        {
            BreweryModel data = new BreweryModel { BreweryId = breweryId, BreweryName = breweryName };
            return data;
        }
        public static BeerModel InitializeBeerData(int beerId, string beerName, decimal AlchoholPerc)
        {
            BeerModel data = new BeerModel { BeerId = beerId, BeerName = beerName, PercentageAlcoholByVolume = AlchoholPerc };
            return data;
        }
        public static BarModel InitializeBarData(int barId, string barName, string barAddress)
        {
            BarModel data = new BarModel { BarId = barId, BarName = barName, BarAddress = barAddress };
            return data;
        }
    }
}
