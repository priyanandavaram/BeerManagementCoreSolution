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
        public void BeersAssociatedWithBar_ShouldReturnData_Service()
        {
            var barWithBeerDetailsById = _barAndBeerService.BeersAssociatedWithBar(3);
            Assert.NotNull(barWithBeerDetailsById);
        }

        [Fact]
        public void BarsWithAssociatedBeers_ShouldReturnData_Service()
        {
            var barsWithBeerDetails = _barAndBeerService.BarsWithAssociatedBeers();
            Assert.NotNull(barsWithBeerDetails);
        }

        [Fact]
        public void BarAndBeerLink_Success_Service()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(2, 4);
            var getResult = _barAndBeerService.BarAndBeerLink(barAndBeerInfo, out string statusMessage);
            var checkIfLinkExists = _dbContext.LinkBarWithBeer.FirstOrDefault(barAndBeer => barAndBeer.BarId == barAndBeerInfo.BarId
                                                                            & barAndBeer.BeerId == barAndBeerInfo.BeerId);
            Assert.True(getResult);
            Assert.NotNull(checkIfLinkExists);
        }

        [Fact]
        public void LinkBarAndBeer_ValidationCheck_Link_Exists_Service()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(3, 4);
            var getResult = _barAndBeerService.BarAndBeerLink(barAndBeerInfo, out string statusMessage);
            var checkIfLinkExists = _dbContext.LinkBarWithBeer.FirstOrDefault(barAndBeer => barAndBeer.BarId == barAndBeerInfo.BarId
                                                                            & barAndBeer.BeerId == barAndBeerInfo.BeerId);
            Assert.False(getResult);
            Assert.NotNull(checkIfLinkExists);
        }
    }
}