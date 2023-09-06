using BeerManagement.Models;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Web.Controllers.Test.TestHelper
{
    public static class StubDataForController
    {
        public static List<BarModel> BarDetails()
        {
            List<BarModel> barDetails = new List<BarModel>
            {
                new BarModel { BarId = 1, BarName = "Red Lion", BarAddress = "London Kings Cross" },
                new BarModel { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds High Street" },
                new BarModel { BarId = 3, BarName = "The Plough", BarAddress = "Manchester City Center 05" },
                new BarModel { BarId = 4, BarName = "Rose & Crown", BarAddress = "Kensington, London" },
                new BarModel { BarId = 5, BarName = "Chequers", BarAddress = "06 Bath Rd, Slough" }
            };
            return barDetails;
        }

        public static List<BreweryModel> BreweryDetails()
        {
            List<BreweryModel> breweryDetails = new List<BreweryModel>
            {
                new BreweryModel { BreweryId = 1, BreweryName = "Carlsberg Group" },
                new BreweryModel { BreweryId = 2, BreweryName = "Sierra Nevada Brewing Co." },
                new BreweryModel { BreweryId = 3, BreweryName = "Bell's Brewery Inc." },
                new BreweryModel { BreweryId = 4, BreweryName = "New Belgium Brewing Company" },
                new BreweryModel { BreweryId = 5, BreweryName = "Flying Dog" }
            };
            return breweryDetails;
        }

        public static List<BeerModel> BeerDetails()
        {
            List<BeerModel> beerDetails = new List<BeerModel>
            {
                new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M },
                new BeerModel { BeerId = 3, BeerName = "Michelob ULTRA", PercentageAlcoholByVolume = 0.11M },
                new BeerModel { BeerId = 4, BeerName = "Modelo Especial", PercentageAlcoholByVolume = 10.1M },
                new BeerModel { BeerId = 5, BeerName = "Stella Artois", PercentageAlcoholByVolume = 36.1M }
            };
            return beerDetails;
        }

        public static List<BreweryWithAssociatedBeersModel> BreweryAndBeerDetailsById()
        {
            var breweryAndBeerDetails = new List<BreweryWithAssociatedBeersModel>
            {
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 1,
                        BreweryName = "Carlsberg Group",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                              new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M }
                        }
                }
            };
            return breweryAndBeerDetails;
        }

        public static List<BarsWithAssociatedBeersModel> BarsAndBeerDetailsById()
        {
            var barAndBeerDetails = new List<BarsWithAssociatedBeersModel>
            {
                new BarsWithAssociatedBeersModel{
                        BarId = 4,
                        BarName = "Rose & Crown",
                        BarAddress = "Kensington, London",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                              new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M }
                        }
                }
            };
            return barAndBeerDetails;
        }

        public static List<BreweryWithAssociatedBeersModel> BreweryAndAssociatedBeers()
        {
            var breweryAndAssociatedBeersInfo = new List<BreweryWithAssociatedBeersModel>
            {
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 1,
                        BreweryName = "Carlsberg Group",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                              new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M }
                        }
                },
                new BreweryWithAssociatedBeersModel{
                        BreweryId = 2,
                        BreweryName = "Sierra Nevada Brewing Co.",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                              new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M }
                        }
                }
            };
            return breweryAndAssociatedBeersInfo;
        }

        public static List<BarsWithAssociatedBeersModel> BarsAndAssociatedBeers()
        {
            var barsAndAssociatedBeersInfo = new List<BarsWithAssociatedBeersModel>
            {
                new BarsWithAssociatedBeersModel{
                        BarId = 4,
                        BarName = "Crown",
                        BarAddress = "Kensington, London",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 1, BeerName = "Coors Light Lager Beer", PercentageAlcoholByVolume = 12.08M },
                              new BeerModel { BeerId = 2, BeerName = "Corona Extra", PercentageAlcoholByVolume = 42.9M }
                        }
                },
                new BarsWithAssociatedBeersModel{
                        BarId = 5,
                        BarName = "Chequers",
                        BarAddress = "06 Bath Rd, Slough",
                        ListOfBeers = new List<BeerModel>
                        {
                              new BeerModel { BeerId = 3, BeerName = "Michelob ULTRA", PercentageAlcoholByVolume = 0.11M },
                              new BeerModel { BeerId = 4, BeerName = "Modelo Especial", PercentageAlcoholByVolume = 10.1M  }
                        }
                }
            };
            return barsAndAssociatedBeersInfo;
        }

        public static BreweryAndBeerModel BreweryAndBeerInfo()
        {
            BreweryAndBeerModel breweryAndBeer = new BreweryAndBeerModel { BreweryId = 1, BeerId = 2 };
            return breweryAndBeer;
        }

        public static BarAndBeerModel BarAndBeerInfo()
        {
            BarAndBeerModel barAndBeer = new BarAndBeerModel { BarId = 1, BeerId = 2 };
            return barAndBeer;
        }

        public static BreweryModel InitializeBreweryData(int breweryId, string breweryName)
        {
            BreweryModel brewery = new BreweryModel { BreweryId = breweryId, BreweryName = breweryName };
            return brewery;
        }

        public static BeerModel InitializeBeerData(int beerId, string beerName, decimal AlcoholPerc)
        {
            BeerModel beer = new BeerModel { BeerId = beerId, BeerName = beerName, PercentageAlcoholByVolume = AlcoholPerc };
            return beer;
        }

        public static BarModel InitializeBarData(int barId, string barName, string barAddress)
        {
            BarModel bar = new BarModel { BarId = barId, BarName = barName, BarAddress = barAddress };
            return bar;
        }
    }
}