using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Models.DataModels;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace BeerManagement.Services.Services
{
    public class BarService : IBarService
    {
        private readonly IUnitOfWork<Bars> unitOfWork;
        private readonly IMapper mapper;

        public BarService(IUnitOfWork<Bars> unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;
            mapper = autoMapper;
        }
        public List<BarModel> GetAllBars()
        {
            var getAllBars = unitOfWork.GenericRepository.GetAllEntityRecords();
            return mapper.Map<List<BarModel>>(getAllBars);
        }

        public BarModel GetBarDetailsById(int id)
        {
            var getBarDetailsById = unitOfWork.GenericRepository.GetEntityDetailsById(id);
            return mapper.Map<BarModel>(getBarDetailsById);
        }

        public bool SaveNewBarDetails(BarModel barInfo, out string statusMessage)
        {
            barInfo.BarId = 0;
            var newBarInfo = mapper.Map<Bars>(barInfo);
            return unitOfWork.GenericRepository.SaveNewRecord(newBarInfo, out statusMessage);
        }

        public bool UpdateBarDetails(BarModel barInfo, out string statusMessage)
        {
            var updatedbarInfo = mapper.Map<Bars>(barInfo);
            return unitOfWork.GenericRepository.UpdateEntityRecord(updatedbarInfo, updatedbarInfo.BarId, out statusMessage);
        }
    }
}
