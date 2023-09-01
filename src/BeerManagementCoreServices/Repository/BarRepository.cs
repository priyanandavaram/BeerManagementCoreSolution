using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Common;
using System.Collections.Generic;
using System.Linq;


namespace BeerManagementCoreServices.Repository
{
    public class BarRepository : IBarRepository
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private readonly IGenericServiceRepository<Bars> _genericServiceRepository;
        public BarRepository(BeerManagementDatabaseContext bmsContext, IGenericServiceRepository<Bars> genericServiceRepository)
        {
            _bmsContext = bmsContext;
            _genericServiceRepository = genericServiceRepository;
        }
        public Bars GetBarDetailsById(int id)
        {
            var barDetailsById = _genericServiceRepository.GetEntityDetailsById(id);

            return barDetailsById;
        }
        public List<Bars> GetAllBars()
        {
            var totalBarDetails = _genericServiceRepository.GetAllEntityRecords();

            return totalBarDetails;
        }
        public string UpdateBarDetails(Bars updateBarDetails)
        {
            if (CheckifBarNameExists(updateBarDetails.BarName))
            {
                return Constants.nameExists;
            }
            else
            {
                var updateResult = _genericServiceRepository.UpdateEntityRecord(updateBarDetails, updateBarDetails.BarId);

                return updateResult;
            }
        }
        public string SaveNewBarDetails(Bars createNewBar)
        {
            if (CheckifBarNameExists(createNewBar.BarName))
            {
                return Constants.nameExists;
            }
            else
            {
                createNewBar.BarId = 0;

                var creationResult = _genericServiceRepository.SaveNewRecord(createNewBar);

                return creationResult;
            }
        }                              
        private bool CheckifBarNameExists(string barName)
        {
            return _bmsContext.Bars.Any(x => x.BarName.ToLower().Trim() == barName.ToLower().Trim());
        }        
    }
}
