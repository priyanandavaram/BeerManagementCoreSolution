using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using BeerManagement.Web.Controllers.Test.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace BeerManagement.Web.Controllers.Test
{
    public class BeerControllerTest
    {
        private readonly Mock<IBeerService> _beerService;
        private readonly BeerController _beerController;
        public string statusMessage = "";
        public BeerControllerTest()
        {
            _beerService = new Mock<IBeerService>();
            _beerController = new BeerController(_beerService.Object);
        }

        [Fact]
        public void BeerDetailsById_ShouldReturnBeerDataById()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.BeerDetailsById(3)).Returns(beerDetails[2]);
            var beerDetailsById = _beerController.BeerDetailsById(3);
            Assert.True(beerDetailsById.GetType().FullName == SendReponse.ApiResponse(beerDetailsById).ToString());
        }

        [Fact]
        public void BeerDetailsById_ShouldReturnNull_WhenBeerIdNotFound()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.BeerDetailsById(10)).Returns(beerDetails.ElementAtOrDefault(9));
            var beerDetailsById = _beerController.BeerDetailsById(10);
            Assert.True(beerDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnBeerDataByAlcoholRange()
        {
            var beerDetails = StubDataForController.BeerDetails();
            var filteredData = beerDetails.Where(beerInfo => beerInfo.PercentageAlcoholByVolume > 10
                                                && beerInfo.PercentageAlcoholByVolume < 40).ToList();
            _beerService.Setup(x => x.AllBeersByAlcoholPercentage(10, 40)).Returns(filteredData);
            var allBeerDetails = _beerController.AllBeersByAlcoholPercentage(10, 40);
            Assert.True(allBeerDetails.GetType().FullName == SendReponse.ApiResponse(allBeerDetails).ToString());
        }

        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnAllBeers_WhenAlcoholRangeNotGiven()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.AllBeersByAlcoholPercentage(0, 0)).Returns(beerDetails);
            var allBeerDetails = _beerController.AllBeersByAlcoholPercentage(0, 0);
            Assert.True(allBeerDetails.GetType().FullName == SendReponse.ApiResponse(allBeerDetails).ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldUpdateBeerDetails_WhenValidInputProvided()
        {
            var beerInfo = StubDataForController.InitializeBeerData(5, "Peroni - New", 0.06M);
            _beerService.Setup(x => x.BeerDetailsUpdate(beerInfo, out statusMessage)).Returns(true);
            var result = _beerController.BeerDetailsUpdate(5, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldReturnBadRequest_WhenBeerNameNotProvided()
        {
            var beerInfo = StubDataForController.InitializeBeerData(1, "", 12.08M);
            var result = _beerController.BeerDetailsUpdate(1, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ShouldReturnSuccess_WhenBeerIdNotFound()
        {
            var beerInfo = new BeerModel { BeerId = 10, BeerName = "Jamaican Beer", PercentageAlcoholByVolume = 5.1M };
            _beerService.Setup(x => x.BeerDetailsUpdate(beerInfo, out statusMessage)).Returns(true);
            var result = _beerController.BeerDetailsUpdate(10, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void NewBeer_ShouldReturnTrue_NewBeerAdded()
        {
            var newBeer = StubDataForController.InitializeBeerData(0, "Coke Zero", 0.01M);
            _beerService.Setup(x => x.NewBeer(newBeer, out statusMessage)).Returns(true);
            var result = _beerController.NewBeer(newBeer);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }

        [Fact]
        public void NewBeer_ShouldReturnBadRequest_WhenBeerNameNotProvided()
        {
            var newBeer = StubDataForController.InitializeBeerData(0, "", 12.08M);
            var result = _beerController.NewBeer(newBeer);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}