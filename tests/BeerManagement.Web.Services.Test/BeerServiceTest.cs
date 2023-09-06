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
        public void AllBeersByAlcoholVolume_ShouldReturnAllBeersWithinGivenRange()
        {
            List<BeerModel> beerDetails = _beerService.AllBeersByAlcoholPercentage(10, 40);
            Assert.NotNull(beerDetails);
            Assert.True(beerDetails[0].PercentageAlcoholByVolume > 10);
            Assert.True(beerDetails[1].PercentageAlcoholByVolume < 40);
        }

        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnAllBeers_WhenNoRangeIsProvided()
        {
            List<BeerModel> beerDetails = _beerService.AllBeersByAlcoholPercentage(0, 0);
            var beerCount = _dbContext.Beers.Count();
            Assert.NotNull(beerDetails);
            Assert.True(beerDetails.Count() == beerCount);
            Assert.True(beerDetails.Any(beerInfo => beerInfo.PercentageAlcoholByVolume > 40 && beerInfo.PercentageAlcoholByVolume < 10) == false);
        }

        [Fact]
        public void BeerDetailsById_ShouldReturnBeerDataById()
        {
            BeerModel beerDetailsById = _beerService.BeerDetailsById(4);
            Assert.NotNull(beerDetailsById);
            Assert.True(beerDetailsById.BeerId == 4);
            Assert.True(beerDetailsById.BeerName == "Modelo Especial");
        }

        [Fact]
        public void BeerDetailsById_ShouldReturnNull_WhenBeerIdNotFound()
        {
            BeerModel beerDetailsById = _beerService.BeerDetailsById(14);
            Assert.Null(beerDetailsById);
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldReturnTrue_BeerDetailsUpdated()
        {
            var beerInfo = StubDataForService.InitializeBeerInfo(9, "Cruise Manie Co.", 16.18M);
            var updateBeerInfo = _beerService.BeerDetailsUpdate(beerInfo, out string statusMessage);
            BeerModel updatedInfo = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(beers => beers.BeerId == 9));
            Assert.True(updateBeerInfo);
            Assert.NotNull(updatedInfo);
            Assert.True(updatedInfo.BeerName == beerInfo.BeerName);
            Assert.True(updatedInfo.PercentageAlcoholByVolume == beerInfo.PercentageAlcoholByVolume);
            Assert.True(statusMessage == "Record updated successfully.");
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldReturnTrue_WhenBeerIdNotFound()
        {
            var beerInfo = StubDataForService.InitializeBeerInfo(15, "Eric Cantona", 1.2M);
            var updateBeerInfo = _beerService.BeerDetailsUpdate(beerInfo, out string statusMessage);
            BeerModel updatedInfo = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(beers => beers.BeerId == 15));
            Assert.True(updateBeerInfo);
            Assert.Null(updatedInfo);           
            Assert.True(statusMessage == "Record not found.");
        }

        [Fact]
        public void NewBeer_ShouldReturnTrue_NewBeerAdded()
        {
            var newBeer = _beerService.NewBeer(StubDataForService.InitializeBeerInfo(0, "Prosecco Province", 2.08M), out string statusMessage);
            BeerModel newBeerData = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(beers => beers.BeerName == "Prosecco Province"));
            Assert.True(newBeer);
            Assert.NotNull(newBeerData);
            Assert.True(newBeerData.BeerId > 0);
            Assert.True(newBeerData.BeerName == "Prosecco Province");
            Assert.True(statusMessage == "Record created successfully.");
        }
    }
}