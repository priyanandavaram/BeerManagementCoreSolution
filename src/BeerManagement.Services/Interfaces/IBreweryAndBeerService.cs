using BeerManagement.Repository.Models;
using BeerManagement.Models.DataModels;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBreweryAndBeerService
    {
        bool LinkBreweryAndBeer(BreweryAndBeerModel breweryAndBeer, out string statusMessage);
        List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId);
        List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers();
    }
}
