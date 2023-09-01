using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace BeerManagementCoreServices.Common
{
    public class GenericServices<T> : IGenericServiceRepository<T> where T : class
    {
        private readonly BeerManagementDatabaseContext _bmsContext;
        private DbSet<T> _dbset { get; set; }
        public GenericServices(BeerManagementDatabaseContext bmsContext)
        {
            _bmsContext = bmsContext;
            _dbset = _bmsContext.Set<T>();
        }
        public T GetEntityDetailsById(int id)
        {
            var getDetailsById = _dbset.Find(id);

            return getDetailsById;
        }
        public List<T> GetAllEntityRecords()
        {
            var getAllEntityRecords = _dbset.ToList();

            return getAllEntityRecords;
        }             
        public string UpdateEntityRecord(T entity, int id)
        {
            if (id != 0)
            {
                var getEntityData = _dbset.Find(id);

                if (getEntityData != null)
                {
                    _bmsContext.Entry(getEntityData).CurrentValues.SetValues(entity);
                    _bmsContext.SaveChanges();
                    return Constants.updateOperation;
                }
                else
                {
                    return Constants.notFound;
                }
            }
            else
            {
                return Constants.notFound;
            }
        }        
        public string SaveNewRecord(T entity)
        {           
            _bmsContext.Entry(entity).State = EntityState.Added;
             _bmsContext.SaveChanges();
             return Constants.createOperation;                                            
        }
        public string LinkBreweryWithBeer(int breweryId, int beerId)
        {
            string insertSql = "Insert into LinkBreweryWithBeer(BreweryId, BeerId) values (@Value1,@Value2)";
            
            _bmsContext.Database.ExecuteSqlCommand(insertSql, new SqlParameter("@Value1", breweryId), new SqlParameter("@Value2", beerId));

            return Constants.linkOperation;                         
        }
        public string LinkBarWithBeer(int barId, int beerId)
        {
            string insertSql = "Insert into LinkBarWithBeer(BarId, BeerId) values (@Value1,@Value2)";

            _bmsContext.Database.ExecuteSqlCommand(insertSql, new SqlParameter("@Value1", barId), new SqlParameter("@Value2", beerId));

            return Constants.linkOperation;
        }
    }
}
