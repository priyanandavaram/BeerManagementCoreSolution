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
    public class BarServiceTest
    {
        private readonly AppDbContextFixture _fixture;
        private readonly IUnitOfWork<Bars> IunitOfWork;
        private IBarService _barService;
        private readonly IMapper mapper;
        private AppDbContext _dbContext;

        public BarServiceTest(AppDbContextFixture fixture)
        {
            _fixture = fixture;
            _dbContext = new AppDbContext(_fixture.Options);
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Bars, BarModel>().ReverseMap();
            }));
            IunitOfWork = new UnitOfWork<Bars>(_dbContext, mapper);
            _barService = new BarService(IunitOfWork, mapper);
        }
        [Fact]
        public void GetAllBars_ShouldReturnData_Service()
        {
            List<BarModel> getAllBarDetails = _barService.GetAllBars();

            Assert.NotNull(getAllBarDetails);
            Assert.True(getAllBarDetails.Count > 4);
        }

        [Fact]
        public void GetBarDetailsById_ShouldReturnData_Service()
        {
            BarModel getBarDetailsById = _barService.GetBarDetailsById(4);

            Assert.NotNull(getBarDetailsById);
            Assert.True(getBarDetailsById.BarId == 4);
            Assert.True(getBarDetailsById.BarName == "Metropolitan Bar & Pub");
            Assert.True(getBarDetailsById.BarAddress == "Manchester High Street 06");
        }
        [Fact]
        public void GetBarDetailsById_ShouldNotReturnData_Service()
        {
            BarModel getBarDetailsById = _barService.GetBarDetailsById(14);

            Assert.Null(getBarDetailsById);           
        }

        [Fact]
        public void UpdateBarDetails_ShouldUpdateRecord_Service()
        {
            var getResult = _barService.UpdateBarDetails(StubDataForService.InitializeBarInfo(2, "Griffin Pub & Chips", "Leeds High street"),out string statusMessage);
            BarModel checkUpdatedInfo = mapper.Map<BarModel>(_dbContext.Bars.FirstOrDefault(x => x.BarId == 2));

           Assert.True(getResult);
           Assert.NotNull(checkUpdatedInfo);
           Assert.True(checkUpdatedInfo.BarId == 2);
           Assert.True(checkUpdatedInfo.BarName == "Griffin Pub & Chips");
           Assert.True(checkUpdatedInfo.BarAddress == "Leeds High street");
        }

        [Fact]
        public void SaveNewBarDetails_Success_Service()
        {
            var getResult = _barService.SaveNewBarDetails(StubDataForService.InitializeBarInfo(0, "Mexican Bar", "Slough"), out string statusMessage);
            BarModel checkInfoForNewId = mapper.Map<BarModel>(_dbContext.Bars.FirstOrDefault(x => x.BarName == "Mexican Bar"));

            Assert.True(getResult);
            Assert.NotNull(checkInfoForNewId);
            Assert.True(checkInfoForNewId.BarId > 0);
            Assert.True(checkInfoForNewId.BarAddress == "Slough");
        }
    }
}
