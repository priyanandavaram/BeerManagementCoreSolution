using BeerManagement.Repository.Interfaces;
using BeerManagement.Services.Automapper;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using System.Collections.Generic;
using AutoMapper;

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
        public List<BreweryModel> GetAllBreweries()
        {
            var getAllBreweries = unitOfWork.GenericRepository.GetAllEntityRecords();
            return mapper.Map<List<BreweryModel>>(getAllBreweries);
        }

        public BreweryModel GetBreweryDetailsById(int id)
        {
            var getBreweryDetailsById = unitOfWork.GenericRepository.GetEntityDetailsById(id);
            return mapper.Map<BreweryModel>(getBreweryDetailsById);
        }

        public bool SaveNewBreweryDetails(BreweryModel createNewBrewery, out string statusMessage)
        {
            createNewBrewery.BreweryId = 0;
            var newBrewery = mapper.Map<Brewery>(createNewBrewery);
            return unitOfWork.GenericRepository.SaveNewRecord(newBrewery, out statusMessage);
        }

        public bool UpdateBreweryDetails(BreweryModel updateBreweryDetails, out string statusMessage)
        {
            var updateBrewery = mapper.Map<Brewery>(updateBreweryDetails);
            return unitOfWork.GenericRepository.UpdateEntityRecord(updateBrewery, updateBrewery.BreweryId, out statusMessage);
        }
    }
}
