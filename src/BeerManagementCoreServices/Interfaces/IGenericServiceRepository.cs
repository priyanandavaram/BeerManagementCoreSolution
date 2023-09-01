using System.Collections.Generic;
using BeerManagementCoreServices.Database;

namespace BeerManagementCoreServices.Interfaces
{
    public interface IGenericServiceRepository<entity>
    {
        entity GetEntityDetailsById(int id);
        List<entity> GetAllEntityRecords();
        string UpdateEntityRecord(entity entity, int id);
        string SaveNewRecord(entity entity);
        string LinkBreweryWithBeer(int breweryId, int beerId);
        string LinkBarWithBeer(int barId, int beerId);
    }
}
