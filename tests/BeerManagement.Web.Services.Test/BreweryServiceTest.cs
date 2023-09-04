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
    public class BreweryServiceTest
    {
        private readonly AppDbContextFixture _fixture;
        private readonly IUnitOfWork<Brewery> IunitOfWork;
        private IBreweryService _breweryService;
        private readonly IMapper mapper;
        private AppDbContext _dbContext;

        public BreweryServiceTest(AppDbContextFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new AppDbContext(_fixture.Options);
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Brewery, BreweryModel>().ReverseMap();
            }));
            IunitOfWork = new UnitOfWork<Brewery>(_dbContext, mapper);
            _breweryService = new BreweryService(IunitOfWork, mapper);
        }
        [Fact]
        public void GetAllBrewery_ShouldReturnData_Service()
        {
            List<BreweryModel> getAllBreweryDetails = _breweryService.GetAllBreweries();

            Assert.NotNull(getAllBreweryDetails);
            Assert.True(getAllBreweryDetails.Count > 4);
        }

        [Fact]
        public void GetBreweryDetailsById_ShouldReturnData_Service()
        {
            BreweryModel getBreweryDetailsById = _breweryService.GetBreweryDetailsById(4);

            Assert.NotNull(getBreweryDetailsById);
            Assert.True(getBreweryDetailsById.BreweryId == 4);
            Assert.True(getBreweryDetailsById.BreweryName == "United Beverages-Leeds");
        }
        [Fact]
        public void GetBreweryDetailsById_ShouldNotReturnData_Service()
        {
            BreweryModel getBreweryDetailsById = _breweryService.GetBreweryDetailsById(14);

            Assert.Null(getBreweryDetailsById);
        }

        [Fact]
        public void UpdateBreweryDetails_ShouldUpdateRecord_Service()
        {
            var getResult = _breweryService.UpdateBreweryDetails(StubDataForService.InitializeBreweryInfo(3, "United Beverages- Central London"), out string statusMessage);
            BreweryModel checkUpdatedInfo = mapper.Map<BreweryModel>(_dbContext.Brewery.FirstOrDefault(x => x.BreweryId == 3));

            Assert.True(getResult);
            Assert.NotNull(checkUpdatedInfo);
            Assert.True(checkUpdatedInfo.BreweryId == 3);
            Assert.True(checkUpdatedInfo.BreweryName == "United Beverages- Central London");
        }

        [Fact]
        public void SaveNewBreweryDetails_Success_Service()
        {
            var createNewBrewery = _breweryService.SaveNewBreweryDetails(StubDataForService.InitializeBreweryInfo( 0, "United Beverages"), out string statusMessage);
            BreweryModel checkInfoForNewId = mapper.Map<BreweryModel>(_dbContext.Brewery.FirstOrDefault(x => x.BreweryName == "United Beverages"));

            Assert.True(createNewBrewery);
            Assert.NotNull(checkInfoForNewId);
            Assert.True(checkInfoForNewId.BreweryId > 0);
        }
    }
}
