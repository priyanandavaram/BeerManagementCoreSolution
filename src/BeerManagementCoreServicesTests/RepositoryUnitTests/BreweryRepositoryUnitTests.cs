using BeerManagementCoreServices.Database;
using BeerManagementCoreServicesTests.Common;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Repository;
using BeerManagementCoreServices.Common;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests.RepositoryUnitTests
{
    [Collection("BeerManagementDatabaseContextCollection")]
    public class BreweryRepositoryUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly Mock<IGenericServiceRepository<Brewery>> _genericRepo;

        private readonly BreweryRepository _breweryRepository;

        public BreweryRepositoryUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _genericRepo = new Mock<IGenericServiceRepository<Brewery>>();

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _breweryRepository = new BreweryRepository(_bmsContext, _genericRepo.Object);
        }
        [Fact]
        public void GetBreweryDetailsById_Data_Found_Repository()
        {
            var breweryDetails = GetBreweryDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(4)).Returns(breweryDetails[3]);

            var getBreweryDetailsById = _breweryRepository.GetBreweryDetailsById(4);

            Assert.NotNull(getBreweryDetailsById);
            Assert.True(breweryDetails[3].Equals(getBreweryDetailsById));
        }
        [Fact]
        public void GetBreweryDetailsById_No_Data_Found_Repository()
        {
            var breweryDetails = GetBreweryDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(10)).Returns(breweryDetails.ElementAtOrDefault(9));

            var getBreweryDetailsById = _breweryRepository.GetBreweryDetailsById(10);

            Assert.Null(getBreweryDetailsById);
        }
        [Fact]
        public void GetAllBreweries_Repository()
        {
            var breweryDetails = GetBreweryDetails();

            _genericRepo.Setup(x => x.GetAllEntityRecords()).Returns(breweryDetails);

            var getAllBreweryDetails = _breweryRepository.GetAllBreweries();

            Assert.NotNull(getAllBreweryDetails);
            Assert.True(getAllBreweryDetails.Count() == 5);
        }
        [Fact]
        public void UpdateBreweryDetails_Success_Repository()
        {
            var updateRecord = new Brewery { BreweryId = 2, BreweryName = "United Beverages-Mosco-Bar" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 2)).Returns(Constants.updateOperation);

            var getResult = _breweryRepository.UpdateBreweryDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_BreweryName_Already_Exists_Repository()
        {
            var updateRecord = new Brewery { BreweryId = 3, BreweryName = "United Beverages-Dallas" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 3)).Returns(Constants.nameExists);

            var getResult = _breweryRepository.UpdateBreweryDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_Id_Not_Found_Repository()
        {
            var updateRecord = new Brewery { BreweryId = 8, BreweryName = "test" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 8)).Returns(Constants.notFound);

            var getResult = _breweryRepository.UpdateBreweryDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBreweryDetails_Success_Repository()
        {
            var newRecord = new Brewery { BreweryId = 7, BreweryName = "Mexical Beverage" };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.createOperation);

            var getResult = _breweryRepository.SaveNewBreweryDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBreweryDetails_ValidationFail_BreweryName_Exists_Repository()
        {
            var newRecord = new Brewery { BreweryId = 8, BreweryName = "United Beverages-Dallas" };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.nameExists);

            var getResult = _breweryRepository.SaveNewBreweryDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        private List<Brewery> GetBreweryDetails()
        {
            List<Brewery> breweryDetails = new List<Brewery>
            {
                new Brewery { BreweryId = 1, BreweryName = "United Beverages-Dallas" },
                new Brewery { BreweryId = 2, BreweryName = "United Beverages-Mosco" },
                new Brewery { BreweryId = 3, BreweryName = "United Beverages-Mexico"},
                new Brewery { BreweryId = 4, BreweryName = "United Beverages-London" },
                new Brewery { BreweryId = 5, BreweryName = "United Beverages-USA" }
            };
            return breweryDetails;
        }
    }
}
