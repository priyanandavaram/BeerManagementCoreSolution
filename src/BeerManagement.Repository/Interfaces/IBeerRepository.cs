using BeerManagement.Repository.DatabaseContext;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBeerRepository
    {
        List<Beers> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
    }
}
