using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.Models;
using BeerManagement.Repository.UnitOfWork;
using BeerManagement.Services.Interfaces;
using BeerManagement.Services.Services;
using BeerManagement.Web.Services.Test.Common;
using BeerManagement.Web.Services.Test.TestHelper;
using System.Linq;
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
        public void AllBeersAssociatedWithBrewery_ShouldReturnData_Service()
        {
            var breweryWithBeerDetailsById = _breweryAndBeerService.AllBeersAssociatedWithBrewery(4);
            Assert.NotNull(breweryWithBeerDetailsById);
        }

        [Fact]
        public void AllBreweriesWithAssociatedBeers_ShouldReturnData_Service()
        {
            var breweryWithBeerDetails = _breweryAndBeerService.AllBreweriesWithAssociatedBeers();
            Assert.NotNull(breweryWithBeerDetails);
        }

        [Fact]
        public void BreweryAndBeerLink_Success_Service()
        {
            var breweryAndBeer = StubDataForService.InitializeBreweryAndBeerInfo(2, 4);
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeer, out string statusMessage);
            var linkExist = _dbContext.LinkBreweryWithBeer.
                                            FirstOrDefault(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == breweryAndBeerInfo.BreweryId
                                            & breweryAndBeerInfo.BeerId == breweryAndBeerInfo.BeerId);
            Assert.True(result);
            Assert.NotNull(linkExist);
        }

        [Fact]
        public void BreweryAndBeerLink_ValidationCheck_Link_Exists_Service()
        {
            var breweryAndBeer = StubDataForService.InitializeBreweryAndBeerInfo(3, 4);
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeer, out string statusMessage);
            var linkExist = _dbContext.LinkBreweryWithBeer.
                                    FirstOrDefault(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == breweryAndBeerInfo.BreweryId
                                    & breweryAndBeerInfo.BeerId == breweryAndBeerInfo.BeerId);
            Assert.False(result);
            Assert.NotNull(linkExist);
        }
    }
}