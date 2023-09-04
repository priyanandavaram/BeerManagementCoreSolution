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

        public List<Beers> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            var beersDetailsByAlcoholPercentage = _dbContext.Beers.Where(B => ((gtAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume > gtAlcoholByVolume)) || gtAlcoholByVolume == 0) && ((ltAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume < ltAlcoholByVolume)) || (ltAlcoholByVolume == 0))).ToList();

            return beersDetailsByAlcoholPercentage;
        }
    }
}
