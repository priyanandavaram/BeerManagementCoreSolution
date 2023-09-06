using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Models;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;

namespace BeerManagement.Services.Services
{
    public class BarAndBeerService : IBarAndBeerService
    {
        private readonly IUnitOfWork<LinkBarWithBeer> unitOfWork;
        private readonly IMapper mapper;
        public BarAndBeerService(IUnitOfWork<LinkBarWithBeer> unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            mapper = autoMapper;
        }

        public List<BarsWithAssociatedBeersModel> BarsWithAssociatedBeers()
        {
            var allbarsWithAssociatedBeers = unitOfWork.BarAndBeerRepository.BarsWithAssociatedBeers();
            return allbarsWithAssociatedBeers;
        }

        public List<BarsWithAssociatedBeersModel> BeersAssociatedWithBar(int barId)
        {
            var allBeersAssociatedWithBar = unitOfWork.BarAndBeerRepository.BeersAssociatedWithBar(barId);
            return allBeersAssociatedWithBar;
        }

        public bool BarAndBeerLink(BarAndBeerModel barAndBeer, out string statusMessage)
        {
            var barAndBeerInfo = mapper.Map<LinkBarWithBeer>(barAndBeer);
            return unitOfWork.BarAndBeerRepository.BarAndBeerLink(barAndBeerInfo, out statusMessage);
        }
    }
}