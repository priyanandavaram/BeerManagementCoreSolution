using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Models;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Interfaces
{
    public interface ILinkBreweryAndBeerRepository
    {
        string LinkBreweryAndBeer(LinkBreweryWithBeer linkWithBeer);
        List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId);
        List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers();
    }
}
