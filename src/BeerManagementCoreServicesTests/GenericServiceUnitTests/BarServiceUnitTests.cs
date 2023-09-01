using BeerManagementCoreServices.Database;
using BeerManagementCoreServicesTests.Common;
using BeerManagementCoreServices.Common;
using System.Linq;
using Xunit;

namespace BeerManagementCoreServicesTests.GenericServiceUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class BarServiceUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly GenericServices<Bars> _service;

        public BarServiceUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _service = new GenericServices<Bars>(_bmsContext);
        }
        [Fact]
        public void GetAllEntityRecords_Data_Found_Service()
        {
            var getAllBarRecords = _service.GetAllEntityRecords();

            Assert.NotEmpty(getAllBarRecords);
            Assert.True(getAllBarRecords.Count() > 4);
        }
        [Fact]
        public void GetEntityDetailsById_Record_Found_Service()
        {
            Bars getBarsRecordById = _service.GetEntityDetailsById(1);

            Assert.True(getBarsRecordById.BarId == 1);
            Assert.True(getBarsRecordById.BarName == "London Bar & Pub");
            Assert.True(getBarsRecordById.BarAddress == "London Kings Cross");
        }
        [Fact]
        public void GetEntityDetailsById_No_Record_Found_Service()
        {
            Bars getBarsRecordById = _service.GetEntityDetailsById(20);

            Assert.Null(getBarsRecordById);
        }
        [Fact]
        public void UpdateEntityRecord_Service()
        {
            var updateBarRecord = new Bars { BarId = 5, BarName = "United club Bar Club", BarAddress = "Reading City Centre" };

            var getResult = _service.UpdateEntityRecord(updateBarRecord, 5);

            Bars checkUpdatedData = _bmsContext.Bars.FirstOrDefault(b => b.BarId == 5);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
            Assert.True(checkUpdatedData.BarName == "United club Bar Club");
            Assert.True(checkUpdatedData.BarAddress == "Reading City Centre");

        }
        [Fact]
        public void UpdateEntityRecord_Validation_Fail_No_Record_Service()
        {
            var updateBarRecord = new Bars { BarId = 7, BarName = "United Club-B'ham", BarAddress = "Euston" };

            var getResult = _service.UpdateEntityRecord(updateBarRecord, 7);

            var checkUpdatedData = _bmsContext.Bars.FirstOrDefault(b => b.BarId == 7);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
            Assert.Null(checkUpdatedData);
        }
        [Fact]
        public void SaveNewRecord_Service()
        {
            var newBarRecord = new Bars { BarId = 6, BarName = "Fish Bar", BarAddress = "Manchester" };

            var getResult = _service.SaveNewRecord(newBarRecord);

            Bars data = _bmsContext.Bars.FirstOrDefault(b => b.BarId == 6);

            Assert.NotNull(getResult);
            Assert.NotNull(data);
            Assert.True(getResult == Constants.createOperation);
            Assert.True(data.BarId == 6);
            Assert.True(data.BarName == "Fish Bar");
            Assert.True(data.BarAddress == "Manchester");
        }
    }
}
