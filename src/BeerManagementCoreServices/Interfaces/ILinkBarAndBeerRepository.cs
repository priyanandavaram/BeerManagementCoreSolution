using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Models;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Interfaces
{
    public interface ILinkBarAndBeerRepository
    {
        string LinkBarAndBeer(LinkBarWithBeer linkWithBeer);
        List<BarWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId);
        List<BarWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers();
    }
}
