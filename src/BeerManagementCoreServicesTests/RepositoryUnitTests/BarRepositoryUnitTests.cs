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
    public class BarRepositoryUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly Mock<IGenericServiceRepository<Bars>> _genericRepo;

        private readonly BarRepository _barRepository;

        public BarRepositoryUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _genericRepo = new Mock<IGenericServiceRepository<Bars>>();

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _barRepository = new BarRepository(_bmsContext, _genericRepo.Object);
        }
        [Fact]
        public void GetBarDetailsById_Data_Found_Repository()
        {
            var barDetails = GetBarDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(4)).Returns(barDetails[3]);

            var getBarDetailsById = _barRepository.GetBarDetailsById(4);

            Assert.NotNull(getBarDetailsById);
            Assert.True(barDetails[3].Equals(getBarDetailsById));
        }
        [Fact]
        public void GetBarDetailsById_No_Data_Found_Repository()
        {
            var barDetails = GetBarDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(10)).Returns(barDetails.ElementAtOrDefault(9));

            var getBarDetailsById = _barRepository.GetBarDetailsById(10);

            Assert.Null(getBarDetailsById);
        }
        [Fact]
        public void GetAllBars_Repository()
        {
            var barDetails = GetBarDetails();

            _genericRepo.Setup(x => x.GetAllEntityRecords()).Returns(barDetails);

            var getAllBarDetails = _barRepository.GetAllBars();

            Assert.NotNull(getAllBarDetails);
            Assert.Equal(getAllBarDetails, barDetails);
            Assert.True(getAllBarDetails.Count() == 5);
        }
        [Fact]
        public void UpdateBarDetails_Success_Repository()
        {
            var updateRecord = new Bars { BarId = 2, BarName = "Griffin Pub - New", BarAddress = "Leeds Met Hotel" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 2)).Returns(Constants.updateOperation);

            var getResult = _barRepository.UpdateBarDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_BarName_Already_Exists_Repository()
        {
            var updateRecord = new Bars { BarId = 1, BarName = "Metropolitan Bar", BarAddress = "Leeds Met Hotel" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 1)).Returns(Constants.nameExists);

            var getResult = _barRepository.UpdateBarDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_Id_Not_Found_Repository()
        {
            var updateRecord = new Bars { BarId = 10, BarName = "London Bar & Pub & Chips", BarAddress = "Leeds" };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 10)).Returns(Constants.notFound);

            var getResult = _barRepository.UpdateBarDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBarDetails_Success_Repository()
        {
            var newRecord = new Bars { BarId = 6, BarName = "Hilife Bar & Pub", BarAddress = "Leeds High Street" };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.createOperation);

            var getResult = _barRepository.SaveNewBarDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBarDetails_ValidationFail_BarName_Exists_Repository()
        {
            var newRecord = new Bars { BarId = 7, BarName = "United club Bar", BarAddress = "Leeds High Street" };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.nameExists);

            var getResult = _barRepository.SaveNewBarDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        private List<Bars> GetBarDetails()
        {
            List<Bars> barDetails = new List<Bars>
            {
                new Bars { BarId = 1, BarName = "London Bar & Pub", BarAddress = "London Kings Cross" },
                new Bars { BarId = 2, BarName = "Griffin Pub", BarAddress = "Leeds City Center" },
                new Bars { BarId = 3, BarName = "Metropolitan Bar", BarAddress = "Manchester High Street 05" },
                new Bars { BarId = 4, BarName = "Metropolitan Bar & Pub", BarAddress = "Manchester High Street 06" },
                new Bars { BarId = 5, BarName = "United club Bar", BarAddress = "Reading" }
            };
            return barDetails;
        }
    }
}
