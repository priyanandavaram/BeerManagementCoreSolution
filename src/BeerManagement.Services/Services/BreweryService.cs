using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;

namespace BeerManagement.Services.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IUnitOfWork<Brewery> unitOfWork;
        private readonly IMapper mapper;
        public BreweryService(IUnitOfWork<Brewery> unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            mapper = autoMapper;
        }

        public List<BreweryModel> AllBreweries()
        {
            var allBreweries = unitOfWork.GenericRepository.AllEntityRecords();
            return mapper.Map<List<BreweryModel>>(allBreweries);
        }

        public BreweryModel BreweryDetailsById(int id)
        {
            var breweryDetailsById = unitOfWork.GenericRepository.EntityDetailsById(id);
            return mapper.Map<BreweryModel>(breweryDetailsById);
        }

        public bool NewBrewery(BreweryModel breweryInfo, out string statusMessage)
        {
            breweryInfo.BreweryId = 0;
            var newBrewery = mapper.Map<Brewery>(breweryInfo);
            return unitOfWork.GenericRepository.NewRecord(newBrewery, out statusMessage);
        }

        public bool BreweryDetailsUpdate(BreweryModel breweryInfo, out string statusMessage)
        {
            var updateBreweryInfo = mapper.Map<Brewery>(breweryInfo);
            return unitOfWork.GenericRepository.EntityRecordUpdate(updateBreweryInfo, updateBreweryInfo.BreweryId, out statusMessage);
        }
    }
}