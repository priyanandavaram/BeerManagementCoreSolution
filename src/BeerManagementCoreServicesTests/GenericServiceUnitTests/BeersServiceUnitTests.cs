using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using BeerManagementCoreServicesTests.Common;
using System.Linq;
using Xunit;


namespace BeerManagementCoreServicesTests.GenericServiceUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class BeersServiceUnitTests
    {
        private  BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly GenericServices<Beers> _service;

        public BeersServiceUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _service = new GenericServices<Beers>(_bmsContext);
        }
        [Fact]
        public void GetAllEntityRecords_Data_Found_Service()
        {          
            var getAllBeerRecords = _service.GetAllEntityRecords();

            Assert.NotEmpty(getAllBeerRecords);
            Assert.True(getAllBeerRecords.Count() > 9);
        }
        [Fact]
        public void GetEntityDetailsById_Record_Found_Service()
        {           
            Beers getBeerRecordById = _service.GetEntityDetailsById(1);

            Assert.True(getBeerRecordById.BeerId == 1);
            Assert.True(getBeerRecordById.BeerName == "Kingfisher");
        }
        [Fact]
        public void GetEntityDetailsById_No_Record_Found_Service()
        {
            Beers getBeerRecordById = _service.GetEntityDetailsById(20);

            Assert.Null(getBeerRecordById);
        }
        [Fact]
        public void UpdateEntityRecord_Service()
        {            
            var updateBeerRecord = new Beers { BeerId = 10, BeerName = "Kingfisher-Gold", PercentageAlcoholByVolume = 11.30M };

            var getResult = _service.UpdateEntityRecord(updateBeerRecord, 10);

            Beers checkUpdatedData = _bmsContext.Beers.FirstOrDefault(b => b.BeerId == 10);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
            Assert.True(checkUpdatedData.BeerName == "Kingfisher-Gold");
        }
        [Fact]
        public void UpdateEntityRecord_Validation_Fail_No_Record_Service()
        {            
            var updateBeerRecord = new Beers { BeerId = 40, BeerName = "Kingfisher-Stella", PercentageAlcoholByVolume = 12.88M };

            var getResult = _service.UpdateEntityRecord(updateBeerRecord, 40);

            var checkUpdatedData = _bmsContext.Beers.FirstOrDefault(b => b.BeerId == 40);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
            Assert.Null(checkUpdatedData);
        }
        [Fact]
        public void SaveNewRecord_Service()
        {
            var newBeerRecord = new Beers { BeerId = 12, BeerName = "Free Bird", PercentageAlcoholByVolume = 0.00M };

            var getResult = _service.SaveNewRecord(newBeerRecord);

            Beers data = _bmsContext.Beers.FirstOrDefault(b => b.BeerId == 12);

            Assert.NotNull(getResult);
            Assert.NotNull(data);
            Assert.True(getResult == Constants.createOperation);
            Assert.True(data.BeerId == 12);
            Assert.True(data.BeerName == "Free Bird");
        }               
    }
}
