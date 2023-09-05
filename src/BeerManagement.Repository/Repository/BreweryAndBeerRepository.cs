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
    public class BreweryAndBeerRepository : IBreweryAndBeerRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper mapper;
        public BreweryAndBeerRepository(AppDbContext dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            mapper = autoMapper;
        }
        public List<BreweryWithAssociatedBeersModel> AllBeersAssociatedWithBrewery(int breweryId)
        {
            var beersAssociatedWithBrewery = _dbContext.LinkBreweryWithBeer.Include(allLinkedEntities => allLinkedEntities.Beer)
                                                                           .Include(allLinkedEntities => allLinkedEntities.Brewery)
                                                                           .Where(allLinkedEntities => allLinkedEntities.BreweryId == breweryId)
                                                                           .ToList();
            return BreweryAndAssociatedBeersInfo(beersAssociatedWithBrewery);
        }

        public List<BreweryWithAssociatedBeersModel> AllBreweriesWithAssociatedBeers()
        {
            var breweryWithAssociatedBeers = _dbContext.LinkBreweryWithBeer.Include(allLinkedEntities => allLinkedEntities.Beer)
                                                                           .Include(allLinkedEntities => allLinkedEntities.Brewery)
                                                                           .ToList();
            return BreweryAndAssociatedBeersInfo(breweryWithAssociatedBeers);
        }

        public bool BreweryAndBeerLink(LinkBreweryWithBeer breweryAndBeerInfo, out string statusMessage)
        {
            if (IsExist(breweryAndBeerInfo.BreweryId, breweryAndBeerInfo.BeerId))
            {
                if (!IsCombinationExist(breweryAndBeerInfo.BreweryId, breweryAndBeerInfo.BeerId))
                {
                    _dbContext.Add(breweryAndBeerInfo);
                    _dbContext.SaveChanges();
                    statusMessage = "Provided brewery and beer Id is linked successfully";
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
                statusMessage = "Provided brewery/beer Id doesn't exists in the database.";
                return false;
            }
        }

        private List<BreweryWithAssociatedBeersModel> BreweryAndAssociatedBeersInfo(List<LinkBreweryWithBeer> totalBreweryAndBars)
        {
            var breweryAndAssociatedBeersInfo = totalBreweryAndBars.GroupBy(breweryInfo => (breweryInfo.BreweryId, breweryInfo.Brewery.BreweryName))
                                        .Select(categoryGroup => new BreweryWithAssociatedBeersModel
                                        {
                                            BreweryId = categoryGroup.Key.BreweryId,
                                            BreweryName = categoryGroup.Key.BreweryName,
                                            ListOfBeers = mapper.Map<List<BeerModel>>(categoryGroup.Select(beerInfo => beerInfo.Beer).ToList())
                                        });
            return breweryAndAssociatedBeersInfo.ToList();
        }

        private bool IsExist(int BreweryId, int beerId)
        {
            if ((_dbContext.Brewery.Any(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == BreweryId)) 
                && (_dbContext.Beers.Any(breweryAndBeerInfo => breweryAndBeerInfo.BeerId == beerId)))
            {
                return true;
            }
            return false;
        }

        private bool IsCombinationExist(int BreweryId, int beerId)
        {
            var exists = _dbContext.LinkBreweryWithBeer.FirstOrDefault(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == BreweryId 
                                                                      && breweryAndBeerInfo.BeerId == beerId);
            return exists != null ? true : false;
        }
    }
}