using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Models.DataModels;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace BeerManagement.Services.Services
{
    public class BeerService : IBeerService
    {
        private readonly IUnitOfWork<Beers> unitOfWork;
        private readonly IMapper mapper;
        public BeerService(IUnitOfWork<Beers> unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            mapper = autoMapper;
        }
        public List<BeerModel> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            var getBeersByAlcoholLevel = unitOfWork.BeerRepository.GetAllBeersByAlchoholPercentage(gtAlcoholByVolume, ltAlcoholByVolume);
            return mapper.Map<List<BeerModel>>(getBeersByAlcoholLevel);
        }

        public BeerModel GetBeerDetailsById(int id)
        {
            var getBeerDetailsById = unitOfWork.GenericRepository.GetEntityDetailsById(id);
            return mapper.Map<BeerModel>(getBeerDetailsById);
        }

        public bool SaveNewBeerDetails(BeerModel beerInfo, out string statusMessage)
        {
            beerInfo.BeerId = 0;
            var newBeerInfo = mapper.Map<Beers>(beerInfo);
            return unitOfWork.GenericRepository.SaveNewRecord(newBeerInfo, out statusMessage);
        }

        public bool UpdateBeerDetails(BeerModel beerInfo, out string statusMessage)
        {
            var updatedBeerInfo = mapper.Map<Beers>(beerInfo);
            return unitOfWork.GenericRepository.UpdateEntityRecord(updatedBeerInfo, updatedBeerInfo.BeerId, out statusMessage);
        }
    }
}
