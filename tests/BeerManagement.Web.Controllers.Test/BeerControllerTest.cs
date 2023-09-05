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
        public void BeerDetailsById_ShouldReturnData_Controller()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.BeerDetailsById(3)).Returns(beerDetails[2]);
            var beerDetailsById = _beerController.BeerDetailsById(3);
            Assert.True(beerDetailsById.GetType().FullName == SendReponse.ApiResponse(beerDetailsById).ToString());
        }

        [Fact]
        public void BeerDetailsById_ShouldNot_ReturnData_Controller()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.BeerDetailsById(10)).Returns(beerDetails.ElementAtOrDefault(9));
            var beerDetailsById = _beerController.BeerDetailsById(10);
            Assert.True(beerDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void BeerDetailsById_Invalid_Input_Controller()
        {
            var beerDetailsById = _beerController.BeerDetailsById(-1);
            Assert.True(beerDetailsById.GetType().FullName == SendReponse.BadRequestObjectResult("BeerId").ToString());
        }

        [Fact]
        public void AllBeersByAlcoholVolume_ShouldReturnData_Controller()
        {
            var beerDetails = StubDataForController.BeerDetails();
            var filteredData = beerDetails.Where(beerInfo => beerInfo.PercentageAlcoholByVolume > 10
                                                && beerInfo.PercentageAlcoholByVolume < 40).ToList();
            _beerService.Setup(x => x.AllBeersByAlchoholPercentage(10, 40)).Returns(filteredData);
            var allBeerDetails = _beerController.AllBeersByAlchoholPercentage(10, 40);
            Assert.True(allBeerDetails.GetType().FullName == SendReponse.ApiResponse(allBeerDetails).ToString());
        }

        [Fact]
        public void AllBeersByAlcoholVolume_WithOptionalValues_ShouldReturnData_Controller()
        {
            var beerDetails = StubDataForController.BeerDetails();
            _beerService.Setup(x => x.AllBeersByAlchoholPercentage(0, 0)).Returns(beerDetails);
            var allBeerDetails = _beerController.AllBeersByAlchoholPercentage(0, 0);
            Assert.True(allBeerDetails.GetType().FullName == SendReponse.ApiResponse(allBeerDetails).ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_Success_Controller()
        {
            var beerInfo = StubDataForController.InitializeBeerData(5, "Peroni - New", 0.06M);
            _beerService.Setup(x => x.BeerDetailsUpdate(beerInfo, out statusMessage)).Returns(true);
            var result = _beerController.BeerDetailsUpdate(5, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ValidationFail_BeerName_Not_Provided_Controller()
        {
            var beerInfo = StubDataForController.InitializeBeerData(1, "", 12.08M);
            var result = _beerController.BeerDetailsUpdate(1, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ValidationFail_Invalid_Id_Controller()
        {
            var beerInfo = StubDataForController.InitializeBeerData(-1, "Stella", 42.9M);
            var result = _beerController.BeerDetailsUpdate(-1, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BeerDetailsUpdate_ValidationFail_BeerId_Not_Found_Controller()
        {
            var beerInfo = new BeerModel { BeerId = 10, BeerName = "Jamaican Beer", PercentageAlcoholByVolume = 5.1M };
            _beerService.Setup(x => x.BeerDetailsUpdate(beerInfo, out statusMessage)).Returns(true);
            var result = _beerController.BeerDetailsUpdate(10, beerInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void NewBeer_Success_Controller()
        {
            var newBeer = StubDataForController.InitializeBeerData(6, "Coke Zero", 0.01M);
            _beerService.Setup(x => x.NewBeer(newBeer, out statusMessage)).Returns(true);
            var result = _beerController.NewBeer(newBeer);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }

        [Fact]
        public void NewBeer_ValidationFail_BeerName_Not_Provided_Controller()
        {
            var newBeer = StubDataForController.InitializeBeerData(1, "", 12.08M);
            var result = _beerController.NewBeer(newBeer);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}