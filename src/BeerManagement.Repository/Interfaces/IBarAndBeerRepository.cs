using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBarAndBeerRepository
    {
        bool BarAndBeerLink(LinkBarWithBeer barAndBeer, out string statusMessage);
        List<BarsWithAssociatedBeersModel> BeersAssociatedWithBar(int barId);
        List<BarsWithAssociatedBeersModel> BarsWithAssociatedBeers();
    }
}