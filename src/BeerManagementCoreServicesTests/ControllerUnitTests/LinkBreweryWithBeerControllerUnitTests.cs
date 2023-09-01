using BeerManagementCoreServices.Controllers;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Common;
using BeerManagementCoreServices.Database;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests.Controllers
{
    public class LinkBreweryWithBeerControllerUnitTests
    {
        private readonly Mock<ILinkBreweryAndBeerRepository> _linkBreweryAndBeerRepository;

        private readonly LinkBreweryAndBeerController _linkBreweryWithBeerController;

        public LinkBreweryWithBeerControllerUnitTests()
        {
            _linkBreweryAndBeerRepository = new Mock<ILinkBreweryAndBeerRepository>();

            _linkBreweryWithBeerController = new LinkBreweryAndBeerController(_linkBreweryAndBeerRepository.Object);
        }
        [Fact]
        public void LinkBreweryAndBeer_Success_Controller()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 1;
            bb.BeerId = 4;

            _linkBreweryAndBeerRepository.Setup(x => x.LinkBreweryAndBeer(bb)).Returns(Constants.linkOperation);

            var getResult = _linkBreweryWithBeerController.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.linkOperation);
        }
        [Fact]
        public void LinkBreweryAndBeer_ValidationFail_BeerId_Not_Found_Controller()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 1;
            bb.BeerId = 8;

            _linkBreweryAndBeerRepository.Setup(x => x.LinkBreweryAndBeer(bb)).Returns(Constants.notFound);

            var getResult = _linkBreweryWithBeerController.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void LinkBreweryAndBeer_ValidationFail_BreweryId_Not_Found_Controller()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 10;
            bb.BeerId = 4;

            _linkBreweryAndBeerRepository.Setup(x => x.LinkBreweryAndBeer(bb)).Returns(Constants.notFound);

            var getResult = _linkBreweryWithBeerController.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void LinkBreweryAndBeer_ValidationFail_Link_Already_Exists_Controller()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 1;
            bb.BeerId = 4;

            _linkBreweryAndBeerRepository.Setup(x => x.LinkBreweryAndBeer(bb)).Returns(Constants.notFound);

            var getResult = _linkBreweryWithBeerController.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
    }
}
