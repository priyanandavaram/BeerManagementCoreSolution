using BeerManagementCoreServicesTests.Common;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using System.Linq;
using Xunit;

namespace BeerManagementCoreServicesTests.GenericServiceUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class BreweryServiceUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly GenericServices<Brewery> _service;

        public BreweryServiceUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _service = new GenericServices<Brewery>(_bmsContext);
        }
        [Fact]
        public void GetAllEntityRecords_Data_Found_Service()
        {
            var getAllBreweryRecords = _service.GetAllEntityRecords();

            Assert.NotEmpty(getAllBreweryRecords);
            Assert.True(getAllBreweryRecords.Count() > 4);
        }
        [Fact]
        public void GetEntityDetailsById_Record_Found_Service()
        {
            Brewery getBreweryRecordById = _service.GetEntityDetailsById(1);

            Assert.True(getBreweryRecordById.BreweryId == 1);
            Assert.True(getBreweryRecordById.BreweryName == "United Beverages-Mexico");
        }
        [Fact]
        public void GetEntityDetailsById_No_Record_Found_Service()
        {
            Brewery getBreweryRecordById = _service.GetEntityDetailsById(20);

            Assert.Null(getBreweryRecordById);
        }
        [Fact]
        public void UpdateEntityRecord_Service()
        {
            var updateBreweryRecord = new Brewery { BreweryId = 5, BreweryName = "United Beverages-B'ham" };

            var getResult = _service.UpdateEntityRecord(updateBreweryRecord, 5);

            Brewery checkUpdatedData = _bmsContext.Brewery.FirstOrDefault(b => b.BreweryId == 5);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
            Assert.True(checkUpdatedData.BreweryName == "United Beverages-B'ham");
        }
        [Fact]
        public void UpdateEntityRecord_Validation_Fail_No_Record_Service()
        {
            var updateBreweryRecord = new Brewery { BreweryId = 7, BreweryName = "United Beverages-Ham" };

            var getResult = _service.UpdateEntityRecord(updateBreweryRecord, 7);

            var checkUpdatedData = _bmsContext.Brewery.FirstOrDefault(b => b.BreweryId == 7);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
            Assert.Null(checkUpdatedData);
        }
        [Fact]
        public void SaveNewRecord_Service()
        {
            var newBreweryRecord = new Brewery { BreweryId = 6, BreweryName = "United Beverages-Durham" };

            var getResult = _service.SaveNewRecord(newBreweryRecord);

            Brewery data = _bmsContext.Brewery.FirstOrDefault(b => b.BreweryId == 6);

            Assert.NotNull(getResult);
            Assert.NotNull(data);
            Assert.True(getResult == Constants.createOperation);
            Assert.True(data.BreweryId == 6);
            Assert.True(data.BreweryName == "United Beverages-Durham");
        }
    }
}
