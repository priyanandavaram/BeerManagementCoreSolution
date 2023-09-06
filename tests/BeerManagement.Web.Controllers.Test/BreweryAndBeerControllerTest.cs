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
        public void BreweryAndBeerLink_LinkedBreweryWithBeer()
        {
            var breweryAndBeer = StubDataForController.BreweryAndBeerInfo();
            _breweryAndBeerService.Setup(x => x.BreweryAndBeerLink(breweryAndBeer, out statusMessage)).Returns(true);
            var result = _breweryAndBeerController.BreweryAndBeerLink(breweryAndBeer);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BeersAssociatedWithBrewery_ShouldReturnAllBeersForBreweryId()
        {
            _breweryAndBeerService.Setup(x => x.BeersAssociatedWithBrewery(1)).Returns(StubDataForController.BreweryAndBeerDetailsById());
            var breweryAndAssociatedBeers = _breweryAndBeerController.BeersAssociatedWithBrewery(1);
            Assert.True(breweryAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(breweryAndAssociatedBeers).ToString());
        }

        [Fact]
        public void BreweriesWithAssociatedBeers_ShouldReturnBreweryAndBeerData()
        {
            _breweryAndBeerService.Setup(x => x.BreweriesWithAssociatedBeers()).Returns(StubDataForController.BreweryAndAssociatedBeers());
            var breweryAndAssociatedBeers = _breweryAndBeerController.BreweriesWithAssociatedBeers();
            Assert.True(breweryAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(breweryAndAssociatedBeers).ToString());
        }
    }
}