using BeerManagement.Repository.DatabaseContext;
using BeerManagement.Repository.UnitOfWork;
using BeerManagement.Services.Services;
using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using BeerManagement.Repository.Interfaces;
using BeerManagement.Web.Services.Test.Common;
using BeerManagement.Web.Services.Test.TestHelper;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        public void GetAllBeersByAlcoholVolume_ShouldReturnData_Service()
        {
            List<BeerModel> getBeerDetails = _beerService.GetAllBeersByAlchoholPercentage(10, 40);

            Assert.NotNull(getBeerDetails);
            Assert.True(getBeerDetails[0].PercentageAlcoholByVolume > 10);
            Assert.True(getBeerDetails[1].PercentageAlcoholByVolume < 40);
        }
        [Fact]
        public void GetAllBeersByAlcoholVolume_ShouldReturnDataWithOptionalParams_Service()
        {
            List<BeerModel> getBeerDetails = _beerService.GetAllBeersByAlchoholPercentage(0, 0);
            var getCount = _dbContext.Beers.Count();

            Assert.NotNull(getBeerDetails);
            Assert.True(getBeerDetails.Count() == getCount);
            Assert.True((getBeerDetails.Any(x => x.PercentageAlcoholByVolume > 40 && x.PercentageAlcoholByVolume < 10) == false));
        }

        [Fact]
        public void GetBeerDetailsById_ShouldReturnData_Service()
        {
            BeerModel getBeerDetailsById = _beerService.GetBeerDetailsById(4);

            Assert.NotNull(getBeerDetailsById);
            Assert.True(getBeerDetailsById.BeerId == 4);
            Assert.True(getBeerDetailsById.BeerName == "Budweiser");
        }
        [Fact]
        public void GetBeerDetailsById_ShouldNotReturnData_Service()
        {
            BeerModel getBeerDetailsById = _beerService.GetBeerDetailsById(14);

            Assert.Null(getBeerDetailsById);          
        }

        [Fact]
        public void UpdateBeerDetails_ShouldUpdateRecord_Service()
        {
            var updateBeerInfo = StubDataForService.InitializeBeerInfo(9, "Sunshine", 16.18M);
            
            var getBeerDetailsById = _beerService.UpdateBeerDetails(updateBeerInfo, out string statusMessage);
            
            BeerModel checkUpdatedInfo = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(x => x.BeerId == 9));

            Assert.True(getBeerDetailsById);
            Assert.NotNull(checkUpdatedInfo);
            Assert.True(checkUpdatedInfo.BeerName == updateBeerInfo.BeerName);
            Assert.True(checkUpdatedInfo.PercentageAlcoholByVolume == updateBeerInfo.PercentageAlcoholByVolume);
        }

        [Fact]
        public void SaveNewBeerDetails_Success_Service()
        {
            var createNewBeer = _beerService.SaveNewBeerDetails(StubDataForService.InitializeBeerInfo(0, "Mexican prosecco", 2.08M), out string statusMessage);
            
            BeerModel checkInfoForNewId = mapper.Map<BeerModel>(_dbContext.Beers.FirstOrDefault(x => x.BeerName == "Mexican prosecco"));

            Assert.True(createNewBeer);
            Assert.NotNull(checkInfoForNewId);
            Assert.True(checkInfoForNewId.BeerId > 0);
            Assert.True(checkInfoForNewId.BeerName == "Mexican prosecco");
        }
    }
}
