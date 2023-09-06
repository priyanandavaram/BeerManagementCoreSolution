using BeerManagement.Repository.DatabaseContext;
using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IBeerRepository
    {
        List<Beers> AllBeersByAlcoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
    }
}