using BeerManagement.Services.Interfaces;
using BeerManagement.Web.Common;
using BeerManagement.Web.Controllers.Test.TestHelper;
using Moq;
using Xunit;

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
        public void BarAndBeerLink_LinkedBarWithBeer()
        {
            var barAndBeerInfo = StubDataForController.BarAndBeerInfo();
            _barAndBeerService.Setup(x => x.BarAndBeerLink(barAndBeerInfo, out statusMessage)).Returns(true);
            var result = _barAndBeerController.BarAndBeerLink(barAndBeerInfo);
            Assert.True(result.GetType().FullName == SendReponse.ApiResponse(result).ToString());
        }

        [Fact]
        public void BeersAssociatedWithBar_ShouldReturnAllBeersForBarId()
        {
            _barAndBeerService.Setup(x => x.BeersAssociatedWithBar(4)).Returns(StubDataForController.BarsAndBeerDetailsById());
            var barAndAssociatedBeers = _barAndBeerController.BeersAssociatedWithBar(4);
            Assert.True(barAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(barAndAssociatedBeers).ToString());
        }

        [Fact]
        public void BarsWithAssociatedBeers_ShouldReturnBarsAndBeersData()
        {
            _barAndBeerService.Setup(x => x.BarsWithAssociatedBeers()).Returns(StubDataForController.BarsAndAssociatedBeers());
            var barsAndAssociatedBeers = _barAndBeerController.BarsWithAssociatedBeers();
            Assert.True(barsAndAssociatedBeers.GetType().FullName == SendReponse.ApiResponse(barsAndAssociatedBeers).ToString());
        }
    }
}