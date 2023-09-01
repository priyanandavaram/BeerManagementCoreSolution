using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Common;
using System.Collections.Generic;
using System.Linq;

namespace BeerManagementCoreServices.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private readonly IGenericServiceRepository<Beers> _genericServiceRepository;

        public BeerRepository(BeerManagementDatabaseContext bmsContext, IGenericServiceRepository<Beers> genericServiceRepository)
        {
            _bmsContext = bmsContext;
            _genericServiceRepository = genericServiceRepository;
        }
        
        public Beers GetBeerDetailsById(int id)
        {            
           var beersDetailsById = _genericServiceRepository.GetEntityDetailsById(id);            

           return beersDetailsById;
        }
        public List<Beers> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume)
        {
           var beersDetailsByAlcoholPercentage = _bmsContext.Beers.Where(B => ((gtAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume > gtAlcoholByVolume)) || gtAlcoholByVolume == 0) && ((ltAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume < ltAlcoholByVolume)) || (ltAlcoholByVolume ==0))).ToList();
        
            return beersDetailsByAlcoholPercentage;
        }
        public string UpdateBeerDetails(Beers updateBeerDetails)
        {
            if(CheckifBeerNameExists(updateBeerDetails.BeerName))
            {
                return Constants.nameExists;
            }
            else
            {
                var updateResult = _genericServiceRepository.UpdateEntityRecord(updateBeerDetails, updateBeerDetails.BeerId);

                return updateResult;
            }
        }
        public string SaveNewBeerDetails(Beers createNewBeer)
        {
            if (CheckifBeerNameExists(createNewBeer.BeerName))
            {
                return Constants.nameExists;
            }
            else
            {
                createNewBeer.BeerId = 0;

                var creationResult = _genericServiceRepository.SaveNewRecord(createNewBeer);

                return creationResult;
            }
        }
        private bool CheckifBeerNameExists(string beerName)
        {
           return _bmsContext.Beers.Any(x => x.BeerName.ToLower().Trim() == beerName.ToLower().Trim());           
        }         
    }
}
