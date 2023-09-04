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
    public class BarControllerTest
    {
        private readonly Mock<IBarService> _barService;

        private readonly BarController _barController;

        public string statusMessage = "";

        public BarControllerTest()
        {
            _barService = new Mock<IBarService>();

            _barController = new BarController(_barService.Object);
        }
        [Fact]
        public void GetBarDetailsById_ShouldReturnData_Controller()
        {
            var barDetails = StubDataForController.GetBarDetails();

            _barService.Setup(x => x.GetBarDetailsById(3)).Returns(barDetails[2]);

            var getbarDetailsById = _barController.GetBarDetailsById(3);

            Assert.True(getbarDetailsById.GetType().FullName == SendReponse.ApiResponse(getbarDetailsById).ToString());
        }
        [Fact]
        public void GetBarDetailsById_ShouldNotReturnData_Controller()
        {
            var barDetails = StubDataForController.GetBarDetails();

            _barService.Setup(x => x.GetBarDetailsById(10)).Returns(barDetails.ElementAtOrDefault(9));

            var getbarDetailsById = _barController.GetBarDetailsById(10);

            Assert.True(getbarDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }
        [Fact]
        public void GetBarDetailsById_Invalid_Input_Controller()
        {
            var checkInvalidInput = _barController.GetBarDetailsById(-1);

            Assert.True(checkInvalidInput.GetType().FullName == SendReponse.BadRequestObjectResult("BarId").ToString());
        }
        [Fact]
        public void GetAllBars_NoData_Controller()
        {
            List<BarModel> bars = new List<BarModel>();

            bars = null;
            
            _barService.Setup(x => x.GetAllBars()).Returns(bars);

            var getAllBarDetails = _barController.GetAllBars();

            Assert.True(getAllBarDetails.GetType().FullName == SendReponse.NoContentFound().ToString());
        }
        [Fact]
        public void GetAllBars_ShouldReturnData_Controller()
        {          
            var barDetails = StubDataForController.GetBarDetails();

            _barService.Setup(x => x.GetAllBars()).Returns(barDetails);

            var getAllBarDetails = _barController.GetAllBars();

            Assert.True(getAllBarDetails.GetType().FullName == SendReponse.ApiResponse(getAllBarDetails).ToString());
        }
        [Fact]
        public void UpdateBarDetails_Success_Controller()
        {
            var updateBarInfo = StubDataForController.InitializeBarData( 2, "Griffin Pub - New", "Leeds Met Hotel");

            _barService.Setup(x => x.UpdateBarDetails(updateBarInfo, out statusMessage)).Returns(true);

            var getResult = _barController.UpdateBarDetails(2, updateBarInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_BarName_Not_Exists_Controller()
        {
            var updateBarInfo = StubDataForController.InitializeBarData(1, "", "London Kings Cross");

            var getResult = _barController.UpdateBarDetails(1, updateBarInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_Invalid_Id_Controller()
        {
            var updateBarInfo = StubDataForController.InitializeBarData(-1, "United club Bar", "Reading");

            var getResult = _barController.UpdateBarDetails(-1, updateBarInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_BarId_Not_Found_Controller()
        {
            var updateBarInfo = StubDataForController.InitializeBarData(10, "London Bar & Pub & Chips", "Leeds");

            _barService.Setup(x => x.UpdateBarDetails(updateBarInfo, out statusMessage)).Returns(true);

            var getResult = _barController.UpdateBarDetails(10, updateBarInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void SaveNewBarDetails_Success_Controller()
        {
            var newRecord = StubDataForController.InitializeBarData(6, "Hilife Bar & Pub", "Leeds High Street");

            _barService.Setup(x => x.SaveNewBarDetails(newRecord, out statusMessage)).Returns(true);

            var getResult = _barController.SaveNewBarDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }
        [Fact]
        public void SaveNewBarDetails_ValidationFail_BarName_Not_exists_Controller()
        {
            var newRecord = StubDataForController.InitializeBarData(7, "", "London Kings Cross");

            var getResult = _barController.SaveNewBarDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        
    }
}
