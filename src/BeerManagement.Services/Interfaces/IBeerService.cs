using BeerManagement.Models;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBeerService
    {
        BeerModel BeerDetailsById(int id);
        List<BeerModel> AllBeersByAlcoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        bool BeerDetailsUpdate(BeerModel beerInfo, out string statusMessage);
        bool NewBeer(BeerModel beerInfo, out string statusMessage);
    }
}