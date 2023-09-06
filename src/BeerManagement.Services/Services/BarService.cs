using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Services.Interfaces;
using System.Collections.Generic;

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

        public List<BarModel> AllBars()
        {
            var allBars = unitOfWork.GenericRepository.AllEntityRecords();
            return mapper.Map<List<BarModel>>(allBars);
        }

        public BarModel BarDetailsById(int id)
        {
            var barDetailsById = unitOfWork.GenericRepository.EntityDetailsById(id);
            return mapper.Map<BarModel>(barDetailsById);
        }

        public bool NewBar(BarModel barInfo, out string statusMessage)
        {
            barInfo.BarId = 0;
            var newBarInfo = mapper.Map<Bars>(barInfo);
            return unitOfWork.GenericRepository.NewRecord(newBarInfo, out statusMessage);
        }

        public bool BarDetailsUpdate(BarModel barInfo, out string statusMessage)
        {
            var updatedbarInfo = mapper.Map<Bars>(barInfo);
            return unitOfWork.GenericRepository.EntityRecordUpdate(updatedbarInfo, updatedbarInfo.BarId, out statusMessage);
        }
    }
}