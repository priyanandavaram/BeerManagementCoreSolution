using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Models;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;
namespace BeerManagement.Services.Services
{
    public class BreweryAndBeerService : IBreweryAndBeerService
    {
        private readonly IUnitOfWork<LinkBreweryWithBeer> unitOfWork;
        private readonly IMapper mapper;
        public BreweryAndBeerService(IUnitOfWork<LinkBreweryWithBeer> unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            mapper = autoMapper;
        }
        public List<BreweryWithAssociatedBeersModel> AllBeersAssociatedWithBrewery(int breweryId)
        {
            var allBeersAssociatedWithBrewery = unitOfWork.BreweryAndBeerRepository.AllBeersAssociatedWithBrewery(breweryId);
            return allBeersAssociatedWithBrewery;
        }

        public List<BreweryWithAssociatedBeersModel> AllBreweriesWithAssociatedBeers()
        {
            var allBreweriesWithAssociatedBeers = unitOfWork.BreweryAndBeerRepository.AllBreweriesWithAssociatedBeers();
            return allBreweriesWithAssociatedBeers;
        }

        public bool BreweryAndBeerLink(BreweryAndBeerModel breweryAndBeer, out string statusMessage)
        {
            var breweryAndBeerInfo = mapper.Map<LinkBreweryWithBeer>(breweryAndBeer);
            return unitOfWork.BreweryAndBeerRepository.BreweryAndBeerLink(breweryAndBeerInfo, out statusMessage);
        }
    }
}