using BeerManagement.Models.DataModels;
using System.Collections.Generic;

namespace BeerManagement.Web.Services.Test.TestHelper
{
    public class StubDataForService
    {       
        public static BarModel InitializeBarInfo(int barId, string barName, string barAddress)
        {
            var barInfo = new BarModel { BarId = barId, BarName = barName, BarAddress = barAddress };
            return barInfo;
        }
        public static BarAndBeerModel InitializeBarAndBeerInfo(int barId, int beerId)
        {
            var barAndBeerInfo = new BarAndBeerModel { BarId = barId, BeerId = beerId };
            return barAndBeerInfo;
        }
        public static BreweryAndBeerModel InitializeBreweryAndBeerInfo(int breweryId, int beerId)
        {
            var breweryAndBeerModel = new BreweryAndBeerModel { BreweryId = breweryId, BeerId = beerId };
            return breweryAndBeerModel;
        }
        public static BeerModel InitializeBeerInfo(int beerId, string beerName, decimal AlcoholPerc)
        {
            var beerInfo = new BeerModel { BeerId = beerId, BeerName = beerName, PercentageAlcoholByVolume = AlcoholPerc };
            return beerInfo;
        }
        public static BreweryModel InitializeBreweryInfo(int breweryId, string breweryName)
        {
            var breweryInfo = new BreweryModel { BreweryId = breweryId, BreweryName = breweryName };
            return breweryInfo;
        }      
    }
}
