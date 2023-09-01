using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using BeerManagementCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerManagementCoreServices.Repository
{
    public class LinkBreweryAndBeerRepository : ILinkBreweryAndBeerRepository
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private readonly IGenericServiceRepository<LinkBreweryWithBeer> _genericServiceRepository;

        public LinkBreweryAndBeerRepository(BeerManagementDatabaseContext bmsContext, IGenericServiceRepository<LinkBreweryWithBeer> genericServiceRepository)
        {
            _bmsContext = bmsContext;
            _genericServiceRepository = genericServiceRepository;
        }
        public List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers()
        {
            var totalBreweriesWithAssociatedBeers = _bmsContext.LinkBreweryWithBeer.Include(c => c.Beer).Include(c => c.Brewery).ToList();

            var structuredData = totalBreweriesWithAssociatedBeers
                                        .GroupBy(a => (a.BreweryId, a.Brewery.BreweryName))
                                        .Select(categoryGroup => new BreweryWithAssociatedBeersModel
                                        {
                                            BreweryId = categoryGroup.Key.BreweryId,
                                            BreweryName = categoryGroup.Key.BreweryName,
                                            ListOfBeers = categoryGroup.Select(a => a.Beer).ToList()
                                        });

            return structuredData.ToList();
        }
        public List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId)
        {
            var breweryWithAssociatedBeers = _bmsContext.LinkBreweryWithBeer.Include(c => c.Beer).Include(c => c.Brewery).Where(c => c.BreweryId == breweryId).ToList();

            var structuredData = breweryWithAssociatedBeers
                                       .GroupBy(a => (a.BreweryId, a.Brewery.BreweryName))
                                       .Select(categoryGroup => new BreweryWithAssociatedBeersModel
                                       {
                                           BreweryId = categoryGroup.Key.BreweryId,
                                           BreweryName = categoryGroup.Key.BreweryName,
                                           ListOfBeers = categoryGroup.Select(a => a.Beer).ToList()
                                       });

            return structuredData.ToList();
        }
        public string LinkBreweryAndBeer(LinkBreweryWithBeer linkWithBeer)
        {
            if ((ValidationCheck(linkWithBeer.BreweryId, linkWithBeer.BeerId)) && (!CheckIfSameCombinationAlreadyExists(linkWithBeer.BreweryId, linkWithBeer.BeerId)))
            {
                var linkResult = _genericServiceRepository.LinkBreweryWithBeer(linkWithBeer.BreweryId, linkWithBeer.BeerId);

                return linkResult;
            }
            else
            {
                return Constants.notFound;
            }
        }
        private bool ValidationCheck(int BreweryId, int beerId)
        {
            if ((_bmsContext.Brewery.Any(x => x.BreweryId == BreweryId)) && (_bmsContext.Beers.Any(x => x.BeerId == beerId)))
            {
                return true;
            }            
                return false;
        }
        private bool CheckIfSameCombinationAlreadyExists(int BreweryId, int beerId)
        {
            var exists = _bmsContext.LinkBreweryWithBeer.FirstOrDefault(x => x.BreweryId == BreweryId && x.BeerId == beerId);

            return exists != null ? true : false;
        }
    }
}
