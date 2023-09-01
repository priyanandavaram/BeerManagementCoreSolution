using BeerManagementCoreServices.Database;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Interfaces
{
    public interface IBeerRepository
    {
        Beers GetBeerDetailsById(int id);
        List<Beers> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        string UpdateBeerDetails(Beers updateBeerDetails);
        string SaveNewBeerDetails(Beers createNewBeer);
    }
}
