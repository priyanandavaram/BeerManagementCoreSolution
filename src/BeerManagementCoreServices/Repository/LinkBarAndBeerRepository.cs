using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using BeerManagementCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BeerManagementCoreServices.Repository
{
    public class LinkBarAndBeerRepository : ILinkBarAndBeerRepository
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private readonly IGenericServiceRepository<LinkBarWithBeer> _genericServiceRepository;
        public LinkBarAndBeerRepository(BeerManagementDatabaseContext bmsContext, IGenericServiceRepository<LinkBarWithBeer> genericServiceRepository)
        {
            _bmsContext = bmsContext;
            _genericServiceRepository = genericServiceRepository;
        }
        public List<BarWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers()
        {
            var totalBarsWithAssociatedBeers = _bmsContext.LinkBarWithBeer.Include(c => c.Beer).Include(c => c.Bar).ToList();

            var structuredData = totalBarsWithAssociatedBeers
                                        .GroupBy(a => (a.BarId, a.Bar.BarName, a.Bar.BarAddress))
                                        .Select(categoryGroup => new BarWithAssociatedBeersModel
                                        {
                                            BarId = categoryGroup.Key.BarId,
                                            BarName = categoryGroup.Key.BarName,
                                            BarAddress = categoryGroup.Key.BarAddress,
                                            ListOfBeers = categoryGroup.Select(a => a.Beer).ToList()
                                        });

            return structuredData.ToList();
        }

        public List<BarWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId)
        {
            var barWithAssociatedBeers = _bmsContext.LinkBarWithBeer.Include(c => c.Beer).Include(c => c.Bar).Where(c => c.BarId == barId).ToList();

            var structuredData = barWithAssociatedBeers
                                       .GroupBy(a => (a.BarId, a.Bar.BarName, a.Bar.BarAddress))
                                       .Select(categoryGroup => new BarWithAssociatedBeersModel
                                       {
                                           BarId = categoryGroup.Key.BarId,
                                           BarName = categoryGroup.Key.BarName,
                                           BarAddress = categoryGroup.Key.BarAddress,
                                           ListOfBeers = categoryGroup.Select(a => a.Beer).ToList()
                                       });

            return structuredData.ToList();
        }

        public string LinkBarAndBeer(LinkBarWithBeer linkWithBeer)
        {
            if ((ValidationCheck(linkWithBeer.BarId, linkWithBeer.BeerId)) && (!CheckIfSameCombinationAlreadyExists(linkWithBeer.BarId, linkWithBeer.BeerId)))
            {
                var linkResult = _genericServiceRepository.LinkBarWithBeer(linkWithBeer.BarId, linkWithBeer.BeerId);

                return linkResult;
            }
            else
            {
                return Constants.notFound;
            }
        }
        private bool ValidationCheck(int barId, int beerId)
        {
            if((_bmsContext.Bars.Any(x => x.BarId == barId)) && (_bmsContext.Beers.Any(x => x.BeerId == beerId)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckIfSameCombinationAlreadyExists(int barId, int beerId)
        {
            var exists = _bmsContext.LinkBarWithBeer.FirstOrDefault(x => x.BarId == barId && x.BeerId == beerId);

            return exists != null ? true : false;
        }

    }
}
