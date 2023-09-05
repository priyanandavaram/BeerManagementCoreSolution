using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using BeerManagement.Web.Controllers.Test.TestHelper;
using Moq;
using Xunit;
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
        public void BreweryAndBeerLink_Success_Controller()
        {
            var breweryAndBeer = StubDataForController.BreweryAndBeerInfo();
            _breweryAndBeerService.Setup(x => x.BreweryAndBeerLink(breweryAndBeer, out statusMessage)).Returns(true);
            var result = _breweryAndBeerController.BreweryAndBeerLink(breweryAndBeer);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void AllBeersAssociatedWithBrewery_ShouldReturnData_Controller()
        {
            _breweryAndBeerService.Setup(x => x.AllBeersAssociatedWithBrewery(1)).Returns(StubDataForController.BreweryAndBeerDetailsById());
            var breweryAndAssociatedBeers = _breweryAndBeerController.AllBeersAssociatedWithBrewery(1);
            Assert.True(breweryAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(breweryAndAssociatedBeers).ToString());
        }

        [Fact]
        public void AllBreweriesWithAssociatedBeers_ShouldReturnData_Controller()
        {
            _breweryAndBeerService.Setup(x => x.AllBreweriesWithAssociatedBeers()).Returns(StubDataForController.BreweryAndAssociatedBeers());
            var breweryAndAssociatedBeers = _breweryAndBeerController.AllBreweriesWithAssociatedBeers();
            Assert.True(breweryAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(breweryAndAssociatedBeers).ToString());
        }
    }
}