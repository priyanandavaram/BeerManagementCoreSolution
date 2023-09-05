using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace BeerManagement.Repository.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly AppDbContext _dbContext;
        public BeerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Beers> AllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            var beersDetailsByAlcoholPercentage = _dbContext.Beers.Where(beerInfo => ((gtAlcoholByVolume != 0
                                                    && (beerInfo.PercentageAlcoholByVolume > gtAlcoholByVolume)) || gtAlcoholByVolume == 0)
                                                    && ((ltAlcoholByVolume != 0 && (beerInfo.PercentageAlcoholByVolume < ltAlcoholByVolume))
                                                    || (ltAlcoholByVolume == 0)))
                                                    .ToList();

            return beersDetailsByAlcoholPercentage;
        }
    }
}