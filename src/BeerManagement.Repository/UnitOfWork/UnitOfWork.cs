using AutoMapper;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Repository;

namespace BeerManagement.Repository.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly AppDbContext appDbContext;
        private readonly IBeerRepository beerRepository;
        private readonly IGenericRepository<T> genericRepository;
        private readonly IBarAndBeerRepository barAndBeerRepository;
        private readonly IBreweryAndBeerRepository breweryAndBeerRepository;
        private readonly IMapper mapper;
        public UnitOfWork(AppDbContext appDbContext, IMapper autoMapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = autoMapper;
        }

        public IBarAndBeerRepository BarAndBeerRepository => barAndBeerRepository ?? new BarAndBeerRepository(appDbContext, mapper);
        public IBreweryAndBeerRepository BreweryAndBeerRepository => breweryAndBeerRepository ?? new BreweryAndBeerRepository(appDbContext, mapper);
        public IGenericRepository<T> GenericRepository => genericRepository ?? new GenericRepository<T>(appDbContext);
        public IBeerRepository BeerRepository => beerRepository ?? new BeerRepository(appDbContext);
    }
}