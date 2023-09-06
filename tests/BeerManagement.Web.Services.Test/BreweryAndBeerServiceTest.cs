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
        public void BeersAssociatedWithBrewery_ShouldReturnAllBeersForBreweryId()
        {
            var breweryWithBeerDetailsById = _breweryAndBeerService.BeersAssociatedWithBrewery(4);
            Assert.NotNull(breweryWithBeerDetailsById);
        }

        [Fact]
        public void BeersAssociatedWithBrewery_ShouldReturnEmptyList_WhenNoBreweryIdFound()
        {
            var breweryWithBeerDetailsById = _breweryAndBeerService.BeersAssociatedWithBrewery(14);
            Assert.NotNull(breweryWithBeerDetailsById);
        }

        [Fact]
        public void BreweriesWithAssociatedBeers_ShouldReturnBreweryAndBeerData()
        {
            var breweryWithBeerDetails = _breweryAndBeerService.BreweriesWithAssociatedBeers();
            Assert.NotNull(breweryWithBeerDetails);
        }

        [Fact]
        public void BreweryAndBeerLink_ShouldReturnTrue_LinkedBreweryWithBeer()
        {
            var breweryAndBeerInfo = StubDataForService.InitializeBreweryAndBeerInfo(2, 4);
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeerInfo, out string statusMessage);
            var isLinkExist = _dbContext.LinkBreweryWithBeer.
                                            FirstOrDefault(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == breweryAndBeerInfo.BreweryId
                                            & breweryAndBeerInfo.BeerId == breweryAndBeerInfo.BeerId);
            Assert.True(result);
            Assert.True(statusMessage == "Provided brewery and beer Id is linked successfully.");
            Assert.NotNull(isLinkExist);
        }

        [Fact]
        public void BreweryAndBeerLink_ShouldReturnFalse_WhenLinkAlreadyExistWithGivenData()
        {
            var breweryAndBeerInfo = StubDataForService.InitializeBreweryAndBeerInfo(3, 4);
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeerInfo, out string statusMessage);
            var isLinkExist = _dbContext.LinkBreweryWithBeer.
                                    FirstOrDefault(breweryAndBeerInfo => breweryAndBeerInfo.BreweryId == breweryAndBeerInfo.BreweryId
                                    & breweryAndBeerInfo.BeerId == breweryAndBeerInfo.BeerId);
            Assert.False(result);
            Assert.True(statusMessage == "Same link already exists in the database.");
            Assert.NotNull(isLinkExist);
        }

        [Fact]
        public void BreweryAndBeerLink_ShouldReturnFalse_WhenBeerIdNotExist()
        {
            var breweryAndBeerInfo = StubDataForService.InitializeBreweryAndBeerInfo(3, 13);
            var result = _breweryAndBeerService.BreweryAndBeerLink(breweryAndBeerInfo, out string statusMessage);
            Assert.False(result);
            Assert.True(statusMessage == "Provided brewery/beer Id doesn't exists in the database.");
        }
    }
}