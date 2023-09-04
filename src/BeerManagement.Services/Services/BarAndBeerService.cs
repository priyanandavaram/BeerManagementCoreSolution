using BeerManagement.Repository.Models;
using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;
using AutoMapper;

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
        public List<BarsWithAssociatedBeersModel> GetAllBarsWithAssociatedBeers()
        {
            var getAllBarsWithAssociatedBeers = unitOfWork.BarAndBeerRepository.GetAllBarsWithAssociatedBeers();
            return getAllBarsWithAssociatedBeers;
        }

        public List<BarsWithAssociatedBeersModel> GetAllBeersAssociatedWithBar(int barId)
        {
            var getAllBeersAssociatedWithBar = unitOfWork.BarAndBeerRepository.GetAllBeersAssociatedWithBar(barId);
            return getAllBeersAssociatedWithBar;
        }

        public bool LinkBarAndBeer(BarAndBeerModel barAndBeer, out string statusMessage)
        {
            var barAndBeerInfo = mapper.Map<LinkBarWithBeer>(barAndBeer);
            return unitOfWork.BarAndBeerRepository.LinkBarAndBeer(barAndBeerInfo,out statusMessage);
        }
    }
}
