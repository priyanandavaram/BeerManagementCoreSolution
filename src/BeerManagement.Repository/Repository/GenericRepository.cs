using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeerManagement.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbset { get; set; }

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();
        }

        public List<T> AllEntityRecords()
        {
            var allEntityRecords = _dbset.ToList();

            return allEntityRecords;
        }

        public T EntityDetailsById(int id)
        {
            var entityDetailsById = _dbset.Find(id);

            return entityDetailsById;
        }

        public bool NewRecord(T entity, out string statusMessage)
        {
            try
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                statusMessage = "Record created successfully.";
                return true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY constraint"))
                {
                    statusMessage = "A record with the same name already exists.";
                    return false;
                }
                statusMessage = "Error occured while creating the record. " + ex.InnerException.Message;
                return false;
            }
        }

        public bool EntityRecordUpdate(T entity, int id, out string statusMessage)
        {
            try
            {
                var entityData = _dbset.Find(id);

                if (entityData != null)
                {
                    _dbContext.Entry(entityData).CurrentValues.SetValues(entity);
                    _dbContext.SaveChanges();
                    statusMessage = "Record updated successfully.";
                    return true;
                }
                else
                {
                    statusMessage = "Record not found.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY constraint"))
                {
                    statusMessage = "A record with the same name already exists.";
                    return false;
                }
                statusMessage = "Error occured while updating the record. " + ex.InnerException.Message;
                return false;
            }
        }
    }
}