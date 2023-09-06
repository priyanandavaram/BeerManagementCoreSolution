using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBreweryAndBeerRepository
    {
        bool BreweryAndBeerLink(LinkBreweryWithBeer breweryAndBeer, out string statusMessage);
        List<BreweryWithAssociatedBeersModel> BeersAssociatedWithBrewery(int breweryId);
        List<BreweryWithAssociatedBeersModel> BreweriesWithAssociatedBeers();
    }
}