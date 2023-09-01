using BeerManagementCoreServices.Common;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BeerManagementCoreServices.Repository
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private readonly IGenericServiceRepository<Brewery> _genericServiceRepository;
        public BreweryRepository(BeerManagementDatabaseContext bmsContext, IGenericServiceRepository<Brewery> genericServiceRepository)
        {
            _bmsContext = bmsContext;
            _genericServiceRepository = genericServiceRepository;
        }
        public Brewery GetBreweryDetailsById(int id)
        {
            var breweryDetailsById = _genericServiceRepository.GetEntityDetailsById(id);

            return breweryDetailsById;
        }
        public List<Brewery> GetAllBreweries()
        {
            var totalBreweryDetails = _genericServiceRepository.GetAllEntityRecords();

            return totalBreweryDetails;
        }
        public string UpdateBreweryDetails(Brewery updateBreweryDetails)
        {
            if (CheckIfBreweryNameExists(updateBreweryDetails.BreweryName))
            {
                return Constants.nameExists;
            }
            else
            {
                var updateResult = _genericServiceRepository.UpdateEntityRecord(updateBreweryDetails, updateBreweryDetails.BreweryId);

                return updateResult;
            }
        }
        public string SaveNewBreweryDetails(Brewery createNewBrewery)
        {
            if (CheckIfBreweryNameExists(createNewBrewery.BreweryName))
            {
                return Constants.nameExists;
            }
            else
            {
                createNewBrewery.BreweryId = 0; 

                var creationResult = _genericServiceRepository.SaveNewRecord(createNewBrewery);

                return creationResult;
            }
        }        
        private bool CheckIfBreweryNameExists(string breweryName)
        {
            return _bmsContext.Brewery.Any(x => x.BreweryName.ToLower().Trim() == breweryName.ToLower().Trim());
        }        
    }
}
