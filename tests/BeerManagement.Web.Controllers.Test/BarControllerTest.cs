using BeerManagement.Models;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using BeerManagement.Web.Controllers.Test.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
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
        public void BarDetailsById_ShouldReturnData_Controller()
        {
            var barDetails = StubDataForController.BarDetails();
            _barService.Setup(x => x.BarDetailsById(3)).Returns(barDetails[2]);
            var barDetailsById = _barController.BarDetailsById(3);
            Assert.True(barDetailsById.GetType().FullName == SendReponse.ApiResponse(barDetailsById).ToString());
        }

        [Fact]
        public void BarDetailsById_ShouldNot_ReturnData_Controller()
        {
            var barDetails = StubDataForController.BarDetails();
            _barService.Setup(x => x.BarDetailsById(10)).Returns(barDetails.ElementAtOrDefault(9));
            var barDetailsById = _barController.BarDetailsById(10);
            Assert.True(barDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void GetAllBars_NoData_Controller()
        {
            List<BarModel> bars = new List<BarModel>();
            bars = null;
            _barService.Setup(x => x.AllBars()).Returns(bars);
            var allBarDetails = _barController.AllBars();
            Assert.True(allBarDetails.GetType().FullName == SendReponse.NoContentFound().ToString());
        }

        [Fact]
        public void GetAllBars_ShouldReturnData_Controller()
        {
            var barDetails = StubDataForController.BarDetails();
            _barService.Setup(x => x.AllBars()).Returns(barDetails);
            var allBarDetails = _barController.AllBars();
            Assert.True(allBarDetails.GetType().FullName == SendReponse.ApiResponse(allBarDetails).ToString());
        }

        [Fact]
        public void BarDetailsUpdate_Success_Controller()
        {
            var barInfo = StubDataForController.InitializeBarData(2, "Griffin Pub - New", "Leeds Met Hotel");
            _barService.Setup(x => x.BarDetailsUpdate(barInfo, out statusMessage)).Returns(true);
            var result = _barController.BarDetailsUpdate(2, barInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BarDetailsUpdate_ValidationFail_BarName_Not_Provided_Controller()
        {
            var barInfo = StubDataForController.InitializeBarData(1, "", "London Kings Cross");
            var result = _barController.BarDetailsUpdate(1, barInfo);
            Assert.True(result.GetType().FullName == SendReponse.BadRequest().ToString());
        }

        [Fact]
        public void UpdateBarDetails_ValidationFail_BarId_Not_Found_Controller()
        {
            var updateBarInfo = StubDataForController.InitializeBarData(10, "London Bar & Pub & Chips", "Leeds");
            _barService.Setup(x => x.BarDetailsUpdate(updateBarInfo, out statusMessage)).Returns(true);
            var getResult = _barController.BarDetailsUpdate(10, updateBarInfo);
            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }

        [Fact]
        public void SaveNewBarDetails_Success_Controller()
        {
            var newRecord = StubDataForController.InitializeBarData(6, "Hilife Bar & Pub", "Leeds High Street");
            _barService.Setup(x => x.NewBar(newRecord, out statusMessage)).Returns(true);
            var getResult = _barController.NewBar(newRecord);
            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }

        [Fact]
        public void SaveNewBarDetails_ValidationFail_BarName_Not_exists_Controller()
        {
            var newRecord = StubDataForController.InitializeBarData(7, "", "London Kings Cross");
            var getResult = _barController.NewBar(newRecord);
            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}