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
    public class BarAndBeerServiceTest
    {
        private readonly AppDbContextFixture _fixture;
        private readonly IUnitOfWork<LinkBarWithBeer> IunitOfWork;
        private IBarAndBeerService _barAndBeerService;
        private readonly IMapper mapper;
        private AppDbContext _dbContext;
        public BarAndBeerServiceTest(AppDbContextFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new AppDbContext(_fixture.Options);
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LinkBarWithBeer, BarsWithAssociatedBeersModel>().ReverseMap();
                cfg.CreateMap<BarAndBeerModel, LinkBarWithBeer>().ReverseMap();
                cfg.CreateMap<Beers, BeerModel>().ReverseMap();
            }));
            IunitOfWork = new UnitOfWork<LinkBarWithBeer>(_dbContext, mapper);
            _barAndBeerService = new BarAndBeerService(IunitOfWork, mapper);
        }

        [Fact]
        public void BeersAssociatedWithBar_ShouldReturnAllBeersForBarId()
        {
            var barWithBeerDetailsById = _barAndBeerService.BeersAssociatedWithBar(3);
            Assert.NotNull(barWithBeerDetailsById);
            Assert.True(barWithBeerDetailsById[0].ListOfBeers.Count() > 2);
        }

        [Fact]
        public void BeersAssociatedWithBar_ShouldReturnEmptyList_WhenNoBarIdFound()
        {
            var barWithBeerDetailsById = _barAndBeerService.BeersAssociatedWithBar(13);
            Assert.NotNull(barWithBeerDetailsById);
        }

        [Fact]
        public void BarsWithAssociatedBeers_ShouldReturnBarAndBeerData()
        {
            var barsWithBeerDetails = _barAndBeerService.BarsWithAssociatedBeers();
            Assert.NotNull(barsWithBeerDetails);
        }

        [Fact]
        public void BarAndBeerLink_ShouldReturnTrue_LinkedBarWithBeer()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(2, 4);
            var result = _barAndBeerService.BarAndBeerLink(barAndBeerInfo, out string statusMessage);
            var isLinkExist = _dbContext.LinkBarWithBeer.FirstOrDefault(barAndBeer => barAndBeer.BarId == barAndBeerInfo.BarId
                                                                            & barAndBeer.BeerId == barAndBeerInfo.BeerId);
            Assert.True(result);
            Assert.True(statusMessage == "Provided bar and beer Id is linked successfully.");
            Assert.NotNull(isLinkExist);
        }

        [Fact]
        public void BarAndBeerLink_ShouldReturnFalse_WhenLinkAlreadyExistForGivenData()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(3, 4);
            var result = _barAndBeerService.BarAndBeerLink(barAndBeerInfo, out string statusMessage);
            var isLinkExist = _dbContext.LinkBarWithBeer.FirstOrDefault(barAndBeer => barAndBeer.BarId == barAndBeerInfo.BarId
                                                                            & barAndBeer.BeerId == barAndBeerInfo.BeerId);
            Assert.False(result);
            Assert.True(statusMessage == "Same link already exists in the database.");
            Assert.NotNull(isLinkExist);
        }

        [Fact]
        public void BarAndBeerLink_ShouldReturnFalse_WhenBeerIdNotExist()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(3, 12);
            var result = _barAndBeerService.BarAndBeerLink(barAndBeerInfo, out string statusMessage);
            Assert.False(result);
            Assert.True(statusMessage == "Provided bar/beer Id doesn't exists in the database.");
        }
    }
}