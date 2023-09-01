using BeerManagementCoreServicesTests.Common;
using BeerManagementCoreServices.Repository;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Common;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests.RepositoryUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class LinkBarWithBeerRepositoryUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly Mock<IGenericServiceRepository<LinkBarWithBeer>> _genericRepo;

        private readonly LinkBarAndBeerRepository _linkBarAndBeerRepository;

        public LinkBarWithBeerRepositoryUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _genericRepo = new Mock<IGenericServiceRepository<LinkBarWithBeer>>();

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _linkBarAndBeerRepository = new LinkBarAndBeerRepository(_bmsContext, _genericRepo.Object);
        }
        [Fact]
        public void LinkBarAndBeer_Success_Repository()
        {
            LinkBarWithBeer bb = new LinkBarWithBeer();
            bb.BarId = 1;
            bb.BeerId = 4;

            _genericRepo.Setup(x => x.LinkBarWithBeer(bb.BarId, bb.BeerId)).Returns(Constants.linkOperation);

            var getResult = _linkBarAndBeerRepository.LinkBarAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.linkOperation);
        }
        [Fact]
        public void LinkBarAndBeer_ValidationFail_Id_Not_Found_Repository()
        {
            LinkBarWithBeer bb = new LinkBarWithBeer();
            bb.BarId = 6;
            bb.BeerId = 8;

            _genericRepo.Setup(x => x.LinkBarWithBeer(bb.BarId, bb.BeerId)).Returns(Constants.notFound);

            var getResult = _linkBarAndBeerRepository.LinkBarAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
    }
}
