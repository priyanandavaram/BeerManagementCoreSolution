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
        public void AllBars_ShouldReturnAllBars()
        {
            List<BarModel> allBarDetails = _barService.AllBars();
            Assert.NotNull(allBarDetails);
            Assert.True(allBarDetails.Count > 4);
        }

        [Fact]
        public void BarDetailsById_ShouldReturnBarDataById()
        {
            BarModel barDetailsById = _barService.BarDetailsById(4);
            Assert.NotNull(barDetailsById);
            Assert.True(barDetailsById.BarId == 4);
            Assert.True(barDetailsById.BarName == "Rose & Crown");
            Assert.True(barDetailsById.BarAddress == "Kensington, London");
        }

        [Fact]
        public void BarDetailsById_ShouldReturnNull_WhenBarIdNotFound()
        {
            BarModel barDetailsById = _barService.BarDetailsById(14);
            Assert.Null(barDetailsById);
        }

        [Fact]
        public void BarDetailsUpdate_ShouldUpdateBarDetails_BarDetailsUpdated()
        {
            var result = _barService.BarDetailsUpdate(StubDataForService.InitializeBarInfo(2, "Queens Arms", "Leeds High street")
                                                    , out string statusMessage);
            BarModel updatedBarInfo = mapper.Map<BarModel>(_dbContext.Bars.FirstOrDefault(barInfo => barInfo.BarId == 2));
            Assert.True(result);
            Assert.NotNull(updatedBarInfo);
            Assert.True(updatedBarInfo.BarId == 2);
            Assert.True(updatedBarInfo.BarName == "Queens Arms");
            Assert.True(updatedBarInfo.BarAddress == "Leeds High street");
            Assert.True(statusMessage == "Record updated successfully.");
        }

        [Fact]
        public void BarDetailsUpdate_ShouldReturnTrue_WhenBarIdNotFound()
        {
            var result = _barService.BarDetailsUpdate(StubDataForService.InitializeBarInfo(10, "The Blue Fox Bar & Pub", "Chester")
                                                    , out string statusMessage);
            BarModel updatedBarInfo = mapper.Map<BarModel>(_dbContext.Bars.FirstOrDefault(barInfo => barInfo.BarId == 10));
            Assert.True(result);
            Assert.Null(updatedBarInfo);
            Assert.True(statusMessage == "Record not found.");
        }

        [Fact]
        public void NewBar_ShouldReturnTrue_NewBarAdded()
        {
            var result = _barService.NewBar(StubDataForService.InitializeBarInfo(0, "The Camel & artichoke", "Colchester"), out string statusMessage);
            BarModel newBarInfo = mapper.Map<BarModel>(_dbContext.Bars.FirstOrDefault(barInfo => barInfo.BarName == "The Camel & artichoke"));
            Assert.True(result);
            Assert.NotNull(newBarInfo);
            Assert.True(newBarInfo.BarId > 0);
            Assert.True(newBarInfo.BarAddress == "Colchester");
            Assert.True(statusMessage == "Record created successfully.");
        }
    }
}