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
        public void BreweryDetailsById_ShouldReturnData_Controller()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.BreweryDetailsById(3)).Returns(breweryDetails[2]);
            var breweryDetailsById = _breweryController.BreweryDetailsById(3);
            Assert.True(breweryDetailsById.GetType().FullName == SendReponse.ApiResponse(breweryDetailsById).ToString());
        }

        [Fact]
        public void BreweryDetailsById_ShouldNot_ReturnData_Controller()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.BreweryDetailsById(10)).Returns(breweryDetails.ElementAtOrDefault(9));
            var breweryDetailsById = _breweryController.BreweryDetailsById(10);
            Assert.True(breweryDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void BreweryDetailsById_Invalid_Input_Controller()
        {
            var breweryDetailsById = _breweryController.BreweryDetailsById(-1);
            Assert.True(breweryDetailsById.GetType().FullName == SendReponse.BadRequestObjectResult("BreweryId").ToString());
        }

        [Fact]
        public void AllBrewery_ShouldReturnData_Controller()
        {
            var breweryDetails = StubDataForController.BreweryDetails();
            _breweryService.Setup(x => x.AllBreweries()).Returns(breweryDetails);
            var allBreweryDetails = _breweryController.AllBreweries();
            Assert.True(allBreweryDetails.GetType().FullName == SendReponse.ApiResponse(allBreweryDetails).ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_Success_Controller()
        {
            var breweryInfo = StubDataForController.InitializeBreweryData(5, "United Beverages-Newcastle");
            _breweryService.Setup(x => x.BreweryDetailsUpdate(breweryInfo, out statusMessage)).Returns(true);
            var result = _breweryController.BreweryDetailsUpdate(5, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ValidationFail_BreweryName_Not_Provided_Controller()
        {
            var breweryInfo = StubDataForController.InitializeBreweryData(1, "");
            var result = _breweryController.BreweryDetailsUpdate(1, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ValidationFail_Invalid_Id_Controller()
        {
            var breweryInfo = StubDataForController.InitializeBreweryData(-1, "Coke Zero Sugar");
            var result = _breweryController.BreweryDetailsUpdate(-1, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void BreweryDetailsUpdate_ValidationFail_BreweryId_Not_Found_Controller()
        {
            var breweryInfo = new BreweryModel { BreweryId = 10, BreweryName = "Coke" };
            _breweryService.Setup(x => x.BreweryDetailsUpdate(breweryInfo, out statusMessage)).Returns(true);
            var result = _breweryController.BreweryDetailsUpdate(10, breweryInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void NewBrewery_Success_Controller()
        {
            var newBrewery = StubDataForController.InitializeBreweryData(6, "Coke Beverages");
            _breweryService.Setup(x => x.NewBrewery(newBrewery, out statusMessage)).Returns(true);
            var result = _breweryController.NewBrewery(newBrewery);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }

        [Fact]
        public void NewBrewery_ValidationFail_BreweryName_Not_Provided_Controller()
        {
            var newBrewery = StubDataForController.InitializeBreweryData(7, "");
            var result = _breweryController.NewBrewery(newBrewery);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}