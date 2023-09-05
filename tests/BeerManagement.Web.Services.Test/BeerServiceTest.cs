using AutoMapper;
using BeerManagement.Models;
using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Repository.UnitOfWork;
using BeerManagement.Services.Interfaces;
using BeerManagement.Services.Services;
using BeerManagement.Web.Services.Test.Common;
using BeerManagement.Web.Services.Test.TestHelper;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace BeerManagement.Web.Services.Test
{
    [Collection("AppDbContextCollection")]
    public class BeerServiceTest
    {
        private readonly AppDbContextFixture _fixture;
        private readonly IUnitOfWork<Beers> IunitOfWork;
        private IBeerService _beerService;
        private readonly IMapper mapper;
        private AppDbContext _dbContext;
        public BeerServiceTest(AppDbContextFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new AppDbContext(_fixture.Options);
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Beers, BeerModel>().ReverseMap();
            }));
            IunitOfWork = new UnitOfWork<Beers>(_dbContext, mapper);
            _beerService = new BeerService(IunitOfWork, mapper);
        }
        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnData_Service()
        {
            List<BeerModel> beerDetails = _beerService.AllBeersByAlchoholPercentage(10, 40);
            Assert.NotNull(beerDetails);
            Assert.True(beerDetails[0].PercentageAlcoholByVolume > 10);
            Assert.True(beerDetails[1].PercentageAlcoholByVolume < 40);
        }

        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnData_WithOptionalParams_Service()
        {
            List<BeerModel> beerDetails = _beerService.AllBeersByAlchoholPercentage(0, 0);
            var getCount = _dbContext.Beers.Count();
            Assert.NotNull(beerDetails);
            Assert.True(beerDetails.Count() == getCount);
            Assert.True((beerDetails.Any(beerInfo => beerInfo.PercentageAlcoholByVolume > 40 && beerInfo.PercentageAlcoholByVolume < 10) == false));
        }

        [Fact]
        public void BeerDetailsById_ShouldReturnData_Service()
        {
            BeerModel beerDetailsById = _beerService.BeerDetailsById(4);
            Assert.NotNull(beerDetailsById);
            Assert.True(beerDetailsById.BeerId == 4);
            Assert.True(beerDetailsById.BeerName == "Modelo Especial");
        }

        [Fact]
        public void BeerDetailsById_ShouldNot_ReturnData_Service()
        {
            BeerModel beerDetailsById = _beerService.BeerDetailsById(14);
            Assert.Null(beerDetailsById);
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldUpdateRecord_Service()
        {
            var beerInfo = StubDataForService.InitializeBeerInfo(9, "Cruise Manie Co.", 16.18M);
            var getBeerDetailsById = _beerService.BeerDetailsUpdate(beerInfo, out string statusMessage);
            BeerModel updatedInfo = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(beers => beers.BeerId == 9));
            Assert.True(getBeerDetailsById);
            Assert.NotNull(updatedInfo);
            Assert.True(updatedInfo.BeerName == beerInfo.BeerName);
            Assert.True(updatedInfo.PercentageAlcoholByVolume == beerInfo.PercentageAlcoholByVolume);
        }

        [Fact]
        public void NewBeer_Success_Service()
        {
            var newBeer = _beerService.NewBeer(StubDataForService.InitializeBeerInfo(0, "Prosecco Province", 2.08M), out string statusMessage);
            BeerModel newBeerData = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(beers => beers.BeerName == "Prosecco Province"));
            Assert.True(newBeer);
            Assert.NotNull(newBeerData);
            Assert.True(newBeerData.BeerId > 0);
            Assert.True(newBeerData.BeerName == "Prosecco Province");
        }
    }
}