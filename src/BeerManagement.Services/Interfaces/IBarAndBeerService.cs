using BeerManagement.Models;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBarAndBeerService
    {
        bool BarAndBeerLink(BarAndBeerModel barAndBeer, out string statusMessage);
        List<BarsWithAssociatedBeersModel> BeersAssociatedWithBar(int barId);
        List<BarsWithAssociatedBeersModel> BarsWithAssociatedBeers();
    }
}