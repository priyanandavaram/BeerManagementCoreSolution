using BeerManagementCoreServices.Database;
using BeerManagementCoreServicesTests.Common;
using BeerManagementCoreServices.Common;
using Xunit;

namespace BeerManagementCoreServicesTests.GenericServiceUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class LinkBarWithBeerServiceUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly GenericServices<LinkBarWithBeer> _service;

        public LinkBarWithBeerServiceUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _service = new GenericServices<LinkBarWithBeer>(_bmsContext);
        }

        [Fact]
        public void LinkBarAndBeer_Success_Repository()
        {
            LinkBarWithBeer obj = new LinkBarWithBeer();
            obj.BarId = 1;
            obj.BeerId = 4;

            var getResult = _service.LinkBarWithBeer(1, 4);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.linkOperation);            
        }       
    }
}
