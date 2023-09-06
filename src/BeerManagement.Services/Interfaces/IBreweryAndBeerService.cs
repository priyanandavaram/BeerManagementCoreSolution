using BeerManagement.Models;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBreweryAndBeerService
    {
        bool BreweryAndBeerLink(BreweryAndBeerModel breweryAndBeer, out string statusMessage);
        List<BreweryWithAssociatedBeersModel> BeersAssociatedWithBrewery(int breweryId);
        List<BreweryWithAssociatedBeersModel> BreweriesWithAssociatedBeers();
    }
}