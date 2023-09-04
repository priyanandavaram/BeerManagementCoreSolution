using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBarAndBeerRepository
    {
        bool LinkBarAndBeer(LinkBarWithBeer barAndBeer, out string statusMessage);
        List<BarsWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId);
        List<BarsWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers();
    }
}
