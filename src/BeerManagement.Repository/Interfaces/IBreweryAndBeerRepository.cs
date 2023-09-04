using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBreweryAndBeerRepository
    {
        bool LinkBreweryAndBeer(LinkBreweryWithBeer breweryAndBeer, out string statusMessage);
        List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId);
        List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers();
    }
}
