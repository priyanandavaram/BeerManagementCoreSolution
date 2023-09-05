using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public List<BarsWithAssociatedBeersModel> BarsWithAssociatedBeers()
        {
            var barsWithAssociatedBeers = _dbContext.LinkBarWithBeer.Include(allLinkedEntities => allLinkedEntities.Beer)
                                                                    .Include(allLinkedEntities => allLinkedEntities.Bar)
                                                                    .ToList();
            return BarsAndAssociatedBeersInfo(barsWithAssociatedBeers);
        }

        public List<BarsWithAssociatedBeersModel> BeersAssociatedWithBar(int barId)
        {
            var beersAssociatedWithBar = _dbContext.LinkBarWithBeer.Include(allLinkedEntities => allLinkedEntities.Beer)
                                                                   .Include(allLinkedEntities => allLinkedEntities.Bar)
                                                                   .Where(allLinkedEntities => allLinkedEntities.BarId == barId)
                                                                   .ToList();
            return BarsAndAssociatedBeersInfo(beersAssociatedWithBar);
        }

        public bool BarAndBeerLink(LinkBarWithBeer barAndBeerInfo, out string statusMessage)
        {
            if (IsExist(barAndBeerInfo.BarId, barAndBeerInfo.BeerId))
            {
                if (!IsCombinationExist(barAndBeerInfo.BarId, barAndBeerInfo.BeerId))
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

        private List<BarsWithAssociatedBeersModel> BarsAndAssociatedBeersInfo(List<LinkBarWithBeer> barsAndBeers)
        {
            var barsAndAssociatedBeersInfo = barsAndBeers.GroupBy(barInfo => (barInfo.BarId, barInfo.Bar.BarName, barInfo.Bar.BarAddress))
                                        .Select(categoryGroup => new BarsWithAssociatedBeersModel
                                        {
                                            BarId = categoryGroup.Key.BarId,
                                            BarName = categoryGroup.Key.BarName,
                                            BarAddress = categoryGroup.Key.BarAddress,
                                            ListOfBeers = mapper.Map<List<BeerModel>>(categoryGroup.Select(beerInfo => beerInfo.Beer).ToList())
                                        });
            return barsAndAssociatedBeersInfo.ToList();
        }

        private bool IsExist(int barId, int beerId)
        {
            if ((_dbContext.Bars.Any(barAndBeerInfo => barAndBeerInfo.BarId == barId))
                && (_dbContext.Beers.Any(barAndBeerInfo => barAndBeerInfo.BeerId == beerId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsCombinationExist(int barId, int beerId)
        {
            var exists = _dbContext.LinkBarWithBeer.FirstOrDefault(barAndBeerInfo => barAndBeerInfo.BarId == barId 
                                                                    && barAndBeerInfo.BeerId == beerId);
            return exists != null ? true : false;
        }
    }
}