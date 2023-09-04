using BeerManagement.Web.Controllers.Test.TestHelper;
using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using BeerManagement.Web.Common;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

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
        public void GetBreweryDetailsById_ShouldReturnData_Controller()
        {
            var breweryDetails = StubDataForController.GetBreweryDetails();

            _breweryService.Setup(x => x.GetBreweryDetailsById(3)).Returns(breweryDetails[2]);

            var getbreweryDetailsById = _breweryController.GetBreweryDetailsById(3);

            Assert.True(getbreweryDetailsById.GetType().FullName == SendReponse.ApiResponse(getbreweryDetailsById).ToString());
        }
        [Fact]
        public void GetBreweryDetailsById_ShouldNotReturnData_Controller()
        {
            var breweryDetails = StubDataForController.GetBreweryDetails();

            _breweryService.Setup(x => x.GetBreweryDetailsById(10)).Returns(breweryDetails.ElementAtOrDefault(9));

            var getbreweryDetailsById = _breweryController.GetBreweryDetailsById(10);

            Assert.True(getbreweryDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }
        [Fact]
        public void GetBreweryDetailsById_Invalid_Input_Controller()
        {
            var getbreweryDetailsById = _breweryController.GetBreweryDetailsById(-1);

            Assert.True(getbreweryDetailsById.GetType().FullName == SendReponse.BadRequestObjectResult("BreweryId").ToString());
        }
        [Fact]
        public void GetAllBrewery_ShouldReturnData_Controller()
        {
            var breweryDetails = StubDataForController.GetBreweryDetails();

            _breweryService.Setup(x => x.GetAllBreweries()).Returns(breweryDetails);

            var getAllBreweryDetails = _breweryController.GetAllBreweries();

            Assert.True(getAllBreweryDetails.GetType().FullName == SendReponse.ApiResponse(getAllBreweryDetails).ToString());
        }
        [Fact]
        public void UpdateBreweryDetails_Success_Controller()
        {
            var updateBreweryInfo = StubDataForController.InitializeBreweryData(5, "United Beverages-Newcastle");

            _breweryService.Setup(x => x.UpdateBreweryDetails(updateBreweryInfo, out statusMessage)).Returns(true);

            var getResult = _breweryController.UpdateBreweryDetails(5, updateBreweryInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_BreweryName_Not_Exists_Controller()
        {
            var updateBreweryInfo = StubDataForController.InitializeBreweryData(1, "");

            var getResult = _breweryController.UpdateBreweryDetails(1, updateBreweryInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_Invalid_Id_Controller()
        {
            var updateBreweryInfo = StubDataForController.InitializeBreweryData(-1, "Coke Zero Sugar");

            var getResult = _breweryController.UpdateBreweryDetails(-1, updateBreweryInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_BreweryId_Not_Found_Controller()
        {
            var updateBreweryInfo = new BreweryModel { BreweryId = 10, BreweryName = "Coke" };

            _breweryService.Setup(x => x.UpdateBreweryDetails(updateBreweryInfo, out statusMessage)).Returns(true);

            var getResult = _breweryController.UpdateBreweryDetails(10, updateBreweryInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void SaveNewBreweryDetails_Success_Controller()
        {
            var newRecord = StubDataForController.InitializeBreweryData( 6, "Coke Beverages");

            _breweryService.Setup(x => x.SaveNewBreweryDetails(newRecord, out statusMessage)).Returns(true);

            var getResult = _breweryController.SaveNewBreweryDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }
        [Fact]
        public void SaveNewBreweryDetails_ValidationFail_BreweryName_Not_exists_Controller()
        {
            var newRecord = StubDataForController.InitializeBreweryData(7, "");

            var getResult = _breweryController.SaveNewBreweryDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }

    }
}
