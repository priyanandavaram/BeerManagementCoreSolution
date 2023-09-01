using BeerManagementCoreServices.Controllers;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests.Controllers
{
    public class LinkBarWithBeerControllerUnitTests
    {
        private readonly Mock<ILinkBarAndBeerRepository> _linkBarAndBeerRepository;

        private readonly LinkBarAndBeerController _linkBarWithBeerController;

        public LinkBarWithBeerControllerUnitTests()
        {
            _linkBarAndBeerRepository = new Mock<ILinkBarAndBeerRepository>();

            _linkBarWithBeerController = new LinkBarAndBeerController(_linkBarAndBeerRepository.Object);
        }
        [Fact]
        public void LinkBarAndBeer_Success_Controller()
        {
            LinkBarWithBeer bb = new LinkBarWithBeer();
            bb.BarId = 1;
            bb.BeerId = 4;

            _linkBarAndBeerRepository.Setup(x => x.LinkBarAndBeer(bb)).Returns(Constants.linkOperation);

            var getResult = _linkBarWithBeerController.LinkBarAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.linkOperation);
        }
        [Fact]
        public void LinkBarAndBeer_ValidationFail_Id_Not_Found_Controller()
        {
            LinkBarWithBeer bb = new LinkBarWithBeer();
            bb.BarId = 6;
            bb.BeerId = 8;

            _linkBarAndBeerRepository.Setup(x => x.LinkBarAndBeer(bb)).Returns(Constants.notFound);

            var getResult = _linkBarWithBeerController.LinkBarAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void LinkBarAndBeer_ValidationFail_Link_Already_Exists_Controller()
        {
            LinkBarWithBeer bb = new LinkBarWithBeer();
            bb.BarId = 1;
            bb.BeerId = 4;

            _linkBarAndBeerRepository.Setup(x => x.LinkBarAndBeer(bb)).Returns(Constants.notFound);

            var getResult = _linkBarWithBeerController.LinkBarAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
    }
}
