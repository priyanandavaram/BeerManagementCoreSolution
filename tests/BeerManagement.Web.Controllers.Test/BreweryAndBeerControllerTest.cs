using BeerManagement.Web.Controllers.Test.TestHelper;
using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using Xunit;
using Moq;


namespace BeerManagement.Web.Controllers.Test
{
    public class BreweryAndBeerControllerTest
    {
        private readonly Mock<IBreweryAndBeerService> _breweryAndBeerService;

        private readonly BreweryAndBeerController _breweryAndBeerController;

        public string statusMessage = "";

        public BreweryAndBeerControllerTest()
        {
            _breweryAndBeerService = new Mock<IBreweryAndBeerService>();

            _breweryAndBeerController = new BreweryAndBeerController(_breweryAndBeerService.Object);
        }
        [Fact]
        public void LinkBreweryAndBeer_Success_Controller()
        {
            var breweryDetails = StubDataForController.BreweryAndBeerInfo();

            _breweryAndBeerService.Setup(x => x.LinkBreweryAndBeer(breweryDetails, out statusMessage)).Returns(true);

            var getBreweryAndBeerDetails = _breweryAndBeerController.LinkBreweryAndBeer(breweryDetails);

            Assert.True(getBreweryAndBeerDetails.GetType().FullName == SendReponse.ApiResponse(getBreweryAndBeerDetails).ToString());
        }
        [Fact]
        public void GetAllBeersAssociatedWithBrewery_ShouldReturnData_Controller()
        {
            _breweryAndBeerService.Setup(x => x.GetAllBeersAssociatedWithBrewery(1)).Returns(StubDataForController.GetBreweryAndBeerDetailsById());

            var getBreweryAndBeerDetailsById = _breweryAndBeerController.GetAllBeersAssociatedWithBrewery(1);

            Assert.True(getBreweryAndBeerDetailsById.GetType().FullName == SendReponse.ApiResponse(getBreweryAndBeerDetailsById).ToString());
        }
        [Fact]
        public void GetAllBreweriesWithAssociatedBeers_ShouldReturnData_Controller()
        {
            var breweryDetails = StubDataForController.BreweryAndBeerInfo();

            _breweryAndBeerService.Setup(x => x.GetAllBreweriesWithAssociatedBeers()).Returns(StubDataForController.GetBreweryAndBeerDetails());

            var getBreweryAndBeerDetails= _breweryAndBeerController.GetAllBreweriesWithAssociatedBeers();

            Assert.True(getBreweryAndBeerDetails.GetType().FullName == SendReponse.ApiResponse(getBreweryAndBeerDetails).ToString());
        }
    }
}
