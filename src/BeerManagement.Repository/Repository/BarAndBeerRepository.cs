using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;


namespace BeerManagement.Repository.Repository
{
    public class BarAndBeerRepository : IBarAndBeerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper mapper;

        public BarAndBeerRepository(AppDbContext dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            mapper = autoMapper;
        }

        public List<BarsWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers()
        {
            var totalBarsWithAssociatedBeers = _dbContext.LinkBarWithBeer.Include(c => c.Beer).Include(c => c.Bar).ToList();

            return GetBarBeersData(totalBarsWithAssociatedBeers);
        }

        public List<BarsWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId)
        {
            var barWithAssociatedBeers = _dbContext.LinkBarWithBeer.Include(c => c.Beer).Include(c => c.Bar).Where(c => c.BarId == barId).ToList();

            return GetBarBeersData(barWithAssociatedBeers);          
        }

        public bool LinkBarAndBeer(LinkBarWithBeer barAndBeerInfo, out string statusMessage)
        {
            if(IsExists(barAndBeerInfo.BarId, barAndBeerInfo.BeerId))
            {
               if(!SameCombinationExists(barAndBeerInfo.BarId, barAndBeerInfo.BeerId))
                {
                    _dbContext.LinkBarWithBeer.Add(barAndBeerInfo);
                    _dbContext.SaveChanges();
                    statusMessage = "Provided bar and beer Id is linked successfully";
                    return true;
                }
                else
                {
                    statusMessage = "Same link already exists in the database.";
                    return false;
                }
            }
            else
            {
                statusMessage = "Provided bar/beer Id doesn't exists in the database.";
                return false;
            }            
        }
        private List<BarsWithAssociatedBeersModel> GetBarBeersData(List<LinkBarWithBeer> totalBarsAndBeers)
        {
            var structuredData = totalBarsAndBeers.GroupBy(a => (a.BarId, a.Bar.BarName, a.Bar.BarAddress))
                                        .Select(categoryGroup => new BarsWithAssociatedBeersModel
                                        {
                                            BarId = categoryGroup.Key.BarId,
                                            BarName = categoryGroup.Key.BarName,
                                            BarAddress = categoryGroup.Key.BarAddress,
                                            ListOfBeers = mapper.Map<List<BeerModel>>(categoryGroup.Select(a => a.Beer).ToList()) 
                                        });

            return structuredData.ToList();
        }
        private bool IsExists(int barId, int beerId)
        {
            if ((_dbContext.Bars.Any(x => x.BarId == barId)) && (_dbContext.Beers.Any(x => x.BeerId == beerId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool SameCombinationExists(int barId, int beerId)
        {
            var exists = _dbContext.LinkBarWithBeer.FirstOrDefault(x => x.BarId == barId && x.BeerId == beerId);

            return exists != null ? true : false;
        }
    }
}
