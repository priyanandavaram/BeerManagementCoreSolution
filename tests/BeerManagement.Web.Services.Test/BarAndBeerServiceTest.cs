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
        public void GetAllBeersWithAssociatedWithBar_ShouldReturnData_Service()
        {
            var getBarWithBeerDetailsById = _barAndBeerService.GetAllBeersAssociatedWithBar(3);

            Assert.NotNull(getBarWithBeerDetailsById);
        }
        [Fact]
        public void GetAllBarsWithAssociatedBeers_ShouldReturnData_Service()
        {
            var getBarsWithBeerDetails = _barAndBeerService.GetAllBarsWithAssociatedBeers();

            Assert.NotNull(getBarsWithBeerDetails);
        }
        [Fact]
        public void LinkBarAndBeer_Success_Service()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(2, 4);

            var getResult = _barAndBeerService.LinkBarAndBeer(barAndBeerInfo, out string statusMessage);

            var checkIfLinkExists = _dbContext.LinkBarWithBeer.FirstOrDefault(x => x.BarId == barAndBeerInfo.BarId & x.BeerId == barAndBeerInfo.BeerId);

            Assert.True(getResult);
            Assert.NotNull(checkIfLinkExists);
        }
        [Fact]
        public void LinkBarAndBeer_ValidationCheck_Link_Exists_Service()
        {
            var barAndBeerInfo = StubDataForService.InitializeBarAndBeerInfo(3,4);

            var getResult = _barAndBeerService.LinkBarAndBeer(barAndBeerInfo, out string statusMessage);

            var checkIfLinkExists = _dbContext.LinkBarWithBeer.FirstOrDefault(x => x.BarId == barAndBeerInfo.BarId & x.BeerId == barAndBeerInfo.BeerId);

            Assert.False(getResult);
            Assert.NotNull(checkIfLinkExists);
        }
    }
}
