using BeerManagement.Web.Services.Test.TestHelper;
using BeerManagement.Web.Services.Test.Common;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.UnitOfWork;
using BeerManagement.Services.Interfaces;
using BeerManagement.Repository.Models;
using BeerManagement.Services.Services;
using BeerManagement.Models.DataModels;
using System.Linq;
using AutoMapper;
using Xunit;


namespace BeerManagement.Web.Services.Test
{
    [Collection("AppDbContextCollection")]
    public class BreweryAndBeerServiceTest
    {
        private readonly AppDbContextFixture _fixture;
        private readonly IUnitOfWork<LinkBreweryWithBeer> IunitOfWork;
        private IBreweryAndBeerService _breweryAndBeerService;
        private readonly IMapper mapper;
        private AppDbContext _dbContext;
        public BreweryAndBeerServiceTest(AppDbContextFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new AppDbContext(_fixture.Options);
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LinkBreweryWithBeer, BreweryWithAssociatedBeersModel>().ReverseMap();
                cfg.CreateMap<BreweryAndBeerModel, LinkBreweryWithBeer>().ReverseMap();
                cfg.CreateMap<Beers, BeerModel>().ReverseMap();
            }));
            IunitOfWork = new UnitOfWork<LinkBreweryWithBeer>(_dbContext, mapper);
            _breweryAndBeerService = new BreweryAndBeerService(IunitOfWork, mapper);
        }

        [Fact]
        public void GetAllBeersAssociatedWithBrewery_ShouldReturnData_Service()
        {
            var getBreweryWithBeerDetailsById = _breweryAndBeerService.GetAllBeersAssociatedWithBrewery(4);

            Assert.NotNull(getBreweryWithBeerDetailsById);
        }
        [Fact]
        public void GetAllBreweriesWithAssociatedBeers_ShouldReturnData_Service()
        {
            var getBreweryWithBeerDetails = _breweryAndBeerService.GetAllBeersAssociatedWithBrewery(3);

            Assert.NotNull(getBreweryWithBeerDetails);
        }
        [Fact]
        public void LinkBreweryAndBeer_Success_Service()
        {
            var breweryAndBeerInfo = StubDataForService.InitializeBreweryAndBeerInfo(2,4);
            
            var getResult = _breweryAndBeerService.LinkBreweryAndBeer(breweryAndBeerInfo, out string statusMessage);
            
            var checkIfLinkExists = _dbContext.LinkBreweryWithBeer.FirstOrDefault(x => x.BreweryId == breweryAndBeerInfo.BreweryId & x.BeerId == breweryAndBeerInfo.BeerId);

            Assert.True(getResult);
            Assert.NotNull(checkIfLinkExists);
        }
        [Fact]
        public void LinkBreweryAndBeer_ValidationCheck_Link_Exists_Service()
        {
            var breweryAndBeerInfo = StubDataForService.InitializeBreweryAndBeerInfo(3,4);
            
            var getResult = _breweryAndBeerService.LinkBreweryAndBeer(breweryAndBeerInfo, out string statusMessage);
            
            var checkIfLinkExists = _dbContext.LinkBreweryWithBeer.FirstOrDefault(x => x.BreweryId == breweryAndBeerInfo.BreweryId & x.BeerId == breweryAndBeerInfo.BeerId);

            Assert.False(getResult);
            Assert.NotNull(checkIfLinkExists);
        }
    }
}
 
