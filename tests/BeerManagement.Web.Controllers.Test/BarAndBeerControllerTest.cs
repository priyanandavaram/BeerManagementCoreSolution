using BeerManagement.Web.Controllers.Test.TestHelper;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Xunit;
using Moq;

namespace BeerManagement.Web.Controllers.Test
{
    public class BarAndBeerControllerTest
    {
        private readonly Mock<IBarAndBeerService> _barAndBeerService;

        private readonly BarAndBeerController _barAndBeerController;

        public string statusMessage = "";

        public BarAndBeerControllerTest()
        {
            _barAndBeerService = new Mock<IBarAndBeerService>();

            _barAndBeerController = new BarAndBeerController(_barAndBeerService.Object);
        }
        [Fact]
        public void LinkBarAndBeer_Success_Controller()
        {
            var barAndBeerDetails = StubDataForController.BarAndBeerInfo();

            _barAndBeerService.Setup(x => x.LinkBarAndBeer(barAndBeerDetails, out statusMessage)).Returns(true);

            var getResult = _barAndBeerController.LinkBarAndBeer(barAndBeerDetails);

            Assert.True(getResult.GetType().FullName == SendReponse.ApiResponse(getResult).ToString());
        }
        [Fact]
        public void GetAllBeersAssociatedWithBar_ShouldReturnData_Controller()
        {
            _barAndBeerService.Setup(x => x.GetAllBeersAssociatedWithBar(4)).Returns(StubDataForController.GetBarsAndBeerDetailsById());

            var getBarAndBeerDetailsById = _barAndBeerController.GetAllBeersAssociatedWithBar(4);

            Assert.True(getBarAndBeerDetailsById.GetType().FullName == SendReponse.ApiResponse(getBarAndBeerDetailsById).ToString());
        }
        [Fact]
        public void GetAllBarsWithAssociatedBeers_ShouldReturnData_Controller()
        {
            var barAndBeerDetails = StubDataForController.GetBarsAndBeerDetails();

            _barAndBeerService.Setup(x => x.GetAllBarsWithAssociatedBeers()).Returns(barAndBeerDetails);

            var getBarAndBeerDetails = _barAndBeerController.GetAllBarsWithAssociatedBeers();

            Assert.True(getBarAndBeerDetails.GetType().FullName == SendReponse.ApiResponse(getBarAndBeerDetails).ToString());
        }
    }
}
