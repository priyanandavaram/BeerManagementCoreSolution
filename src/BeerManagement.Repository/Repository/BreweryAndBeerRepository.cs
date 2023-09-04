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
    public class BreweryAndBeerRepository : IBreweryAndBeerRepository 
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper mapper;

        public BreweryAndBeerRepository(AppDbContext dbContext, IMapper autoMapper)
        {
            _dbContext = dbContext;
            mapper = autoMapper;
        }

        public List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId)
        {
            var breweryWithAssociatedBeers = _dbContext.LinkBreweryWithBeer.Include(c => c.Beer).Include(c => c.Brewery).Where(c => c.BreweryId == breweryId).ToList();

            return GetBreweryBeersData(breweryWithAssociatedBeers);
        }

        public List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers()
        {
            var totalBreweriesWithAssociatedBeers = _dbContext.LinkBreweryWithBeer.Include(c => c.Beer).Include(c => c.Brewery).ToList();

            return GetBreweryBeersData(totalBreweriesWithAssociatedBeers);
        }

        public bool LinkBreweryAndBeer(LinkBreweryWithBeer breweryAndBeerInfo, out string statusMessage)
        {  
            if(IsExists(breweryAndBeerInfo.BreweryId, breweryAndBeerInfo.BeerId))
            {
                if(!SameCombinationExists(breweryAndBeerInfo.BreweryId, breweryAndBeerInfo.BeerId))
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
        private List<BreweryWithAssociatedBeersModel> GetBreweryBeersData(List<LinkBreweryWithBeer> totalBreweryAndBars)
        {
            var structuredData = totalBreweryAndBars.GroupBy(a => (a.BreweryId, a.Brewery.BreweryName))
                                        .Select(categoryGroup => new BreweryWithAssociatedBeersModel
                                        {
                                            BreweryId = categoryGroup.Key.BreweryId,
                                            BreweryName = categoryGroup.Key.BreweryName,
                                            ListOfBeers = mapper.Map<List<BeerModel>>(categoryGroup.Select(a => a.Beer).ToList())
                                        });

            return structuredData.ToList();           
        }
        private bool IsExists(int BreweryId, int beerId)
        {
            if ((_dbContext.Brewery.Any(x => x.BreweryId == BreweryId)) && (_dbContext.Beers.Any(x => x.BeerId == beerId)))
            {
                return true;
            }
            return false;
        }
        private bool SameCombinationExists(int BreweryId, int beerId)
        {
            var exists = _dbContext.LinkBreweryWithBeer.FirstOrDefault(x => x.BreweryId == BreweryId && x.BeerId == beerId);

            return exists != null ? true : false;
        }
    }
}
