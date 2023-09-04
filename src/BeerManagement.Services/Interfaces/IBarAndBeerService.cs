using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Models;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBarAndBeerService
    {
        bool LinkBarAndBeer(BarAndBeerModel barAndBeer, out string statusMessage);
        List<BarsWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId);
        List<BarsWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers();
    }
}
