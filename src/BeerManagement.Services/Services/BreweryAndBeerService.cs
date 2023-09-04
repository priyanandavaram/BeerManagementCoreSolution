using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Models;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;
using AutoMapper;

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

        public List<BreweryWithAssociatedBeersModel> GetAllBeersAssociatedWithBrewery(int breweryId)
        {
            var getAllBeersAssociatedWithBrewery = unitOfWork.BreweryAndBeerRepository.GetAllBeersAssociatedWithBrewery(breweryId);
            return getAllBeersAssociatedWithBrewery;
        }

        public List<BreweryWithAssociatedBeersModel> GetAllBreweriesWithAssociatedBeers()
        {
            var getAllBreweriesWithAssociatedBeers = unitOfWork.BreweryAndBeerRepository.GetAllBreweriesWithAssociatedBeers();
            return getAllBreweriesWithAssociatedBeers;
        }

        public bool LinkBreweryAndBeer(BreweryAndBeerModel breweryAndBeer, out string statusMessage)
        {
           var breweryAndBeerInfo = mapper.Map<LinkBreweryWithBeer>(breweryAndBeer);
           return unitOfWork.BreweryAndBeerRepository.LinkBreweryAndBeer(breweryAndBeerInfo, out statusMessage);           
        }
    }
}
