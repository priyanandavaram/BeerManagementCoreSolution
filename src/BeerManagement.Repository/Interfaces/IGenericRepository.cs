using System.Collections.Generic;

namespace BeerManagement.Repository.Interfaces
{
    public interface IGenericRepository<entity>
    {
        entity GetEntityDetailsById(int id);
        List<entity> GetAllEntityRecords();
        bool UpdateEntityRecord(entity entity, int id, out string statusMessage);
        bool SaveNewRecord(entity entity, out string statusMessage);
    }
}
