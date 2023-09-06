using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;

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

        public List<BeerModel> AllBeersByAlcoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
            var beersByAlcoholLevel = unitOfWork.BeerRepository.AllBeersByAlcoholPercentage(gtAlcoholByVolume, ltAlcoholByVolume);
            return mapper.Map<List<BeerModel>>(beersByAlcoholLevel);
        }

        public BeerModel BeerDetailsById(int id)
        {
            var beerDetailsById = unitOfWork.GenericRepository.EntityDetailsById(id);
            return mapper.Map<BeerModel>(beerDetailsById);
        }

        public bool NewBeer(BeerModel beerInfo, out string statusMessage)
        {
            beerInfo.BeerId = 0;
            var newBeerInfo = mapper.Map<Beers>(beerInfo);
            return unitOfWork.GenericRepository.NewRecord(newBeerInfo, out statusMessage);
        }

        public bool BeerDetailsUpdate(BeerModel beerInfo, out string statusMessage)
        {
            var updatedBeerInfo = mapper.Map<Beers>(beerInfo);
            return unitOfWork.GenericRepository.EntityRecordUpdate(updatedBeerInfo, updatedBeerInfo.BeerId, out statusMessage);
        }
    }
}