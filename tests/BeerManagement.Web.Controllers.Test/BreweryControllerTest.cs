using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using BeerManagement.Web.Controllers.Test.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace BeerManagement.Web.Controllers.Test
{
    public class BreweryControllerTest
    {
        private readonly Mock<IBreweryService> _breweryService;
        private readonly BreweryController _breweryController;
        public string statusMessage = "";
        public BreweryControllerTest()
        {
            _breweryService = new Mock<IBreweryService>();
            _breweryController = new BreweryController(_breweryService.Object);
        }

        [Fact]
        public void BreweryDetailsById_ShouldReturnBreweryDataById()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.BreweryDetailsById(3)).Returns(breweryDetails[2]);
            var breweryDetailsById = _breweryController.BreweryDetailsById(3);
            Assert.True(breweryDetailsById.GetType().FullName == SendReponse.ApiResponse(breweryDetailsById).ToString());
        }

        [Fact]
        public void BreweryDetailsById_ShouldReturnNull_WhenBreweryIdNotFound()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.BreweryDetailsById(10)).Returns(breweryDetails.ElementAtOrDefault(9));
            var breweryDetailsById = _breweryController.BreweryDetailsById(10);
            Assert.True(breweryDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void AllBrewery_ShouldReturnAllBreweryData()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.AllBreweries()).Returns(breweryDetails);
            var allBreweryDetails = _breweryController.AllBreweries();
            Assert.True(allBreweryDetails.GetType().FullName == SendReponse.ApiResponse(allBreweryDetails).ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ShouldUpdateBreweryDetails_WhenValidInputProvided()
        {
            var breweryInfo = StubDataForController.InitializeBreweryData(5, "United Beverages-Newcastle");
            _breweryService.Setup(x => x.BreweryDetailsUpdate(breweryInfo, out statusMessage)).Returns(true);
            var result = _breweryController.BreweryDetailsUpdate(5, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ShouldReturnBadRequest_WhenBreweryNameNotProvided()
        {
            var breweryInfo = StubDataForController.InitializeBreweryData(1, "");
            var result = _breweryController.BreweryDetailsUpdate(1, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ShouldReturnSuccess_WhenBreweryIdNotFound()
        {
            var breweryInfo = new BreweryModel { BreweryId = 10, BreweryName = "Coke" };
            _breweryService.Setup(x => x.BreweryDetailsUpdate(breweryInfo, out statusMessage)).Returns(true);
            var result = _breweryController.BreweryDetailsUpdate(10, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void NewBrewery_ShouldReturnTrue_NewBreweryAdded()
        {
            var newBrewery = StubDataForController.InitializeBreweryData(6, "Coke Beverages");
            _breweryService.Setup(x => x.NewBrewery(newBrewery, out statusMessage)).Returns(true);
            var result = _breweryController.NewBrewery(newBrewery);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }

        [Fact]
        public void NewBrewery_ShouldReturnBadRequest_WhenBreweryNameNotProvided()
        {
            var newBrewery = StubDataForController.InitializeBreweryData(7, "");
            var result = _breweryController.NewBrewery(newBrewery);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}