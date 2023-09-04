using BeerManagement.Web.Controllers.Test.TestHelper;
using BeerManagement.Services.Interfaces;
using BeerManagement.Models.DataModels;
using BeerManagement.Web.Common;
using System.Linq;
using Xunit;
using Moq;

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
        public void GetBeerDetailsById_ShouldReturnData_Controller()
        {
            var beerDetails = StubDataForController.GetBeerDetails();

            _beerService.Setup(x => x.GetBeerDetailsById(3)).Returns(beerDetails[2]);

            var getbeerDetailsById = _beerController.GetBeerDetailsById(3);

            Assert.True(getbeerDetailsById.GetType().FullName == SendReponse.ApiResponse(getbeerDetailsById).ToString());
        }
        [Fact]
        public void GetBeerDetailsById_ShouldNotReturnData_Controller()
        {
            var beerDetails = StubDataForController.GetBeerDetails();

            _beerService.Setup(x => x.GetBeerDetailsById(10)).Returns(beerDetails.ElementAtOrDefault(9));

            var getbeerDetailsById = _beerController.GetBeerDetailsById(10);

            Assert.True(getbeerDetailsById.GetType().FullName == SendReponse.NoContentFound().ToString());
        }
        [Fact]
        public void GetBeerDetailsById_Invalid_Input_Controller()
        {
            var getbeerDetailsById = _beerController.GetBeerDetailsById(-1);

            Assert.True(getbeerDetailsById.GetType().FullName == SendReponse.BadRequestObjectResult("BeerId").ToString());
        }
        [Fact]
        public void GetAllBeersByAlcoholVolume_ShouldReturnData_Controller()
        {            
            var beerDetails = StubDataForController.GetBeerDetails();

            var filteredData = beerDetails.Where(x => x.PercentageAlcoholByVolume > 10 && x.PercentageAlcoholByVolume < 40).ToList();

            _beerService.Setup(x => x.GetAllBeersByAlchoholPercentage(10, 40)).Returns(filteredData);

            var getAllBeerDetails = _beerController.GetAllBeersByAlchoholPercentage(10, 40);

            Assert.True(getAllBeerDetails.GetType().FullName == SendReponse.ApiResponse(getAllBeerDetails).ToString());
        }
        [Fact]
        public void GetAllBeersByAlcoholVolume_WithOptionalValues_ShouldReturnData_Controller()
        {
            var beerDetails = StubDataForController.GetBeerDetails();

            _beerService.Setup(x => x.GetAllBeersByAlchoholPercentage(0, 0)).Returns(beerDetails);

            var getAllBeerDetails = _beerController.GetAllBeersByAlchoholPercentage(0, 0);

            Assert.True(getAllBeerDetails.GetType().FullName == SendReponse.ApiResponse(getAllBeerDetails).ToString());
        }
        [Fact]
        public void UpdateBeerDetails_Success_Controller()
        {
            var updateBeerInfo = StubDataForController.InitializeBeerData(5, "Peroni - New", 0.06M);

            _beerService.Setup(x => x.UpdateBeerDetails(updateBeerInfo, out statusMessage)).Returns(true);

            var getResult = _beerController.UpdateBeerDetails(5, updateBeerInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_BeerName_Not_Exists_Controller()
        {
            var updateBeerInfo = StubDataForController.InitializeBeerData(1, "", 12.08M);

            var getResult = _beerController.UpdateBeerDetails(1, updateBeerInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_Invalid_Id_Controller()
        {
            var updateBeerInfo = StubDataForController.InitializeBeerData( -1, "Stella", 42.9M);

            var getResult = _beerController.UpdateBeerDetails(-1, updateBeerInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_BeerId_Not_Found_Controller()
        {
            var updateBeerInfo = new BeerModel { BeerId = 10 , BeerName = "Jamaican Beer", PercentageAlcoholByVolume = 5.1M};

            _beerService.Setup(x => x.UpdateBeerDetails(updateBeerInfo, out statusMessage)).Returns(true);

            var getResult = _beerController.UpdateBeerDetails(10, updateBeerInfo);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void SaveNewBeerDetails_Success_Controller()
        {
            var newRecord = StubDataForController.InitializeBeerData( 6, "Coke Zero", 0.01M);

            _beerService.Setup(x => x.SaveNewBeerDetails(newRecord,out statusMessage)).Returns(true);

            var getResult = _beerController.SaveNewBeerDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(true).ToString());
        }
        [Fact]
        public void SaveNewBeerDetails_ValidationFail_BeerName_Not_exists_Controller()
        {
            var newRecord = StubDataForController.InitializeBeerData(1, "", 12.08M);

            var getResult = _beerController.SaveNewBeerDetails(newRecord);

            Assert.True(getResult.GetType().FullName == SendReponse.BadRequest().ToString());
        }
    }
}
