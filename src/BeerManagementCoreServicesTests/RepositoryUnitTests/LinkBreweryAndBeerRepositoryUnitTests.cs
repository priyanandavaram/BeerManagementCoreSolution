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
    public class LinkBreweryWithBeerRepositoryUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly Mock<IGenericServiceRepository<LinkBreweryWithBeer>> _genericRepo;

        private readonly LinkBreweryAndBeerRepository _linkBreweryAndBeerRepository;

        public LinkBreweryWithBeerRepositoryUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _genericRepo = new Mock<IGenericServiceRepository<LinkBreweryWithBeer>>();

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _linkBreweryAndBeerRepository = new LinkBreweryAndBeerRepository(_bmsContext, _genericRepo.Object);
        }
        [Fact]
        public void LinkBreweryAndBeer_Success_Repository()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 1;
            bb.BeerId = 4;

            _genericRepo.Setup(x => x.LinkBreweryWithBeer(bb.BreweryId, bb.BeerId)).Returns(Constants.linkOperation);

            var getResult = _linkBreweryAndBeerRepository.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.linkOperation);            
        }
        [Fact]
        public void LinkBreweryAndBeer_ValidationFail_Id_Not_Found_Repository()
        {
            LinkBreweryWithBeer bb = new LinkBreweryWithBeer();
            bb.BreweryId = 6;
            bb.BeerId = 8;

            _genericRepo.Setup(x => x.LinkBreweryWithBeer(bb.BreweryId, bb.BeerId)).Returns(Constants.notFound);

            var getResult = _linkBreweryAndBeerRepository.LinkBreweryAndBeer(bb);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }       
    }
}
