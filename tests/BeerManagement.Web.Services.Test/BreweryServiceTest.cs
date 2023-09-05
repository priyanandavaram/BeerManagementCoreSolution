﻿using AutoMapper;
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
        public void AllBrewery_ShouldReturnData_Service()
        {
            List<BreweryModel> allBreweryDetails = _breweryService.AllBreweries();
            Assert.NotNull(allBreweryDetails);
            Assert.True(allBreweryDetails.Count > 4);
        }

        [Fact]
        public void BreweryDetailsById_ShouldReturnData_Service()
        {
            BreweryModel breweryDetailsById = _breweryService.BreweryDetailsById(4);
            Assert.NotNull(breweryDetailsById);
            Assert.True(breweryDetailsById.BreweryId == 4);
            Assert.True(breweryDetailsById.BreweryName == "New Belgium Brewing Company");
        }

        [Fact]
        public void BreweryDetailsById_ShouldNot_ReturnData_Service()
        {
            BreweryModel breweryDetailsById = _breweryService.BreweryDetailsById(14);
            Assert.Null(breweryDetailsById);
        }

        [Fact]
        public void BreweryDetailsUpdate_ShouldUpdateRecord_Service()
        {
            var result = _breweryService.BreweryDetailsUpdate(StubDataForService.InitializeBreweryInfo(3, "Stella London Inc.")
                                                                , out string statusMessage);
            BreweryModel updatedInfo = mapper.Map<BreweryModel>(_dbContext.Brewery.FirstOrDefault(breweryInfo => breweryInfo.BreweryId == 3));
            Assert.True(result);
            Assert.NotNull(updatedInfo);
            Assert.True(updatedInfo.BreweryId == 3);
            Assert.True(updatedInfo.BreweryName == "Stella London Inc.");
        }

        [Fact]
        public void NewBrewery_Success_Service()
        {
            var newBrewery = _breweryService.NewBrewery(StubDataForService.InitializeBreweryInfo(0, "Inc 5 Corporation")
                                                        , out string statusMessage);
            BreweryModel savedInfo = mapper.Map<BreweryModel>(_dbContext.Brewery.FirstOrDefault(x => x.BreweryName == "Inc 5 Corporation"));
            Assert.True(newBrewery);
            Assert.NotNull(savedInfo);
            Assert.True(savedInfo.BreweryId > 0);
        }
    }
}