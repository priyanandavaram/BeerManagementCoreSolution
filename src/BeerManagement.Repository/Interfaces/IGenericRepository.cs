using System.Collections.Generic;
namespace BeerManagement.Repository.Interfaces
{
    public interface IGenericRepository<entity>
    {
        entity EntityDetailsById(int id);
        List<entity> AllEntityRecords();
        bool EntityRecordUpdate(entity entity, int id, out string statusMessage);
        bool NewRecord(entity entity, out string statusMessage);
    }
}