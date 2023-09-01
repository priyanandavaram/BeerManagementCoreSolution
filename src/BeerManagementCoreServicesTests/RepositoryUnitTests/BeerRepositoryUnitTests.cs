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
    public class BeerRepositoryUnitTests
    {
        private BeerManagementDatabaseContext _bmsContext;

        private readonly BeerManagementDatabaseContextFixture _fixture;

        private readonly Mock<IGenericServiceRepository<Beers>> _genericRepo;

        private readonly BeerRepository _beerRepository;

        public BeerRepositoryUnitTests(BeerManagementDatabaseContextFixture fixture)
        {
            _fixture = fixture;

            _genericRepo = new Mock<IGenericServiceRepository<Beers>>();

            _bmsContext = new BeerManagementDatabaseContext(_fixture.Options);

            _beerRepository = new BeerRepository(_bmsContext, _genericRepo.Object);
        }

        [Fact]
        public void GetBeerDetailsById_Data_Found_Repository()
        {
            var beerDetails = GetBeerDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(4)).Returns(beerDetails[3]);

            var getbeerDetailsById = _beerRepository.GetBeerDetailsById(4);

            Assert.NotNull(getbeerDetailsById);
            Assert.True(beerDetails[3].Equals(getbeerDetailsById));
        }
        [Fact]
        public void GetBeerDetailsById_No_Data_Found_Repository()
        {
            var beerDetails = GetBeerDetails();

            _genericRepo.Setup(x => x.GetEntityDetailsById(10)).Returns(beerDetails.ElementAtOrDefault(9));

            var getbeerDetailsById = _beerRepository.GetBeerDetailsById(10);

            Assert.Null(getbeerDetailsById);
        }
        [Fact]
        public void GetAllBeersByAlchoholPercentage_With_Values_Repository()
        {
            var getBeerDetailsByAlcoholPercentage = _beerRepository.GetAllBeersByAlchoholPercentage(10, 40);

            Assert.NotNull(getBeerDetailsByAlcoholPercentage);
            Assert.True(getBeerDetailsByAlcoholPercentage.Count() > 0);
            Assert.True(getBeerDetailsByAlcoholPercentage[0].PercentageAlcoholByVolume > 10);
            Assert.True(getBeerDetailsByAlcoholPercentage[1].PercentageAlcoholByVolume > 10);
            Assert.True(getBeerDetailsByAlcoholPercentage[2].PercentageAlcoholByVolume < 40);
        }
        [Fact]
        public void GetAllBeersByAlchoholPercentage_With_Optional_Values_Repository()
        {            
            var getBeerDetailsByAlcoholPercentage = _beerRepository.GetAllBeersByAlchoholPercentage(0, 0);

            Assert.NotNull(getBeerDetailsByAlcoholPercentage);
            Assert.True(getBeerDetailsByAlcoholPercentage.Count() > 5);
        }
        [Fact]
        public void UpdateBeerDetails_Success_Repository()
        {
            var updateRecord = new Beers { BeerId = 2, BeerName = "Stella - Gold", PercentageAlcoholByVolume = 4.08M };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 2)).Returns(Constants.updateOperation);

            var getResult = _beerRepository.UpdateBeerDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.updateOperation);
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_BeerName_Already_Exists_Repository()
        {
            var updateRecord = new Beers { BeerId = 4, BeerName = "Peroni", PercentageAlcoholByVolume = 4.08M };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 4)).Returns(Constants.nameExists);

            var getResult = _beerRepository.UpdateBeerDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_Id_NotFound_Repository()
        {
            var updateRecord = new Beers { BeerId = 10, BeerName = "Test Beer", PercentageAlcoholByVolume = 0.0M };

            _genericRepo.Setup(x => x.UpdateEntityRecord(updateRecord, 10)).Returns(Constants.notFound);

            var getResult = _beerRepository.UpdateBeerDetails(updateRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBeerDetails_Success_Repository()
        {
            var newRecord = new Beers { BeerId = 7, BeerName = "Merina", PercentageAlcoholByVolume = 0.08M };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.createOperation);

            var getResult = _beerRepository.SaveNewBeerDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBeerDetails_ValidationFail_BeerName_Already_Exists_Repository()
        {
            var newRecord = new Beers { BeerId = 8, BeerName = "Budweiser", PercentageAlcoholByVolume = 0.08M };

            _genericRepo.Setup(x => x.SaveNewRecord(newRecord)).Returns(Constants.nameExists);

            var getResult = _beerRepository.SaveNewBeerDetails(newRecord);

            Assert.NotNull(getResult);
            Assert.True(getResult == Constants.nameExists);
        }
        private List<Beers> GetBeerDetails()
        {
            List<Beers> beerDetails = new List<Beers>
            {
                new Beers { BeerId = 1, BeerName = "Kingfisher", PercentageAlcoholByVolume = 12.08M },
                new Beers { BeerId = 2, BeerName = "Stella", PercentageAlcoholByVolume = 42.9M },
                new Beers { BeerId = 3, BeerName = "Corona", PercentageAlcoholByVolume = 0.11M },
                new Beers { BeerId = 4, BeerName = "Budweiser", PercentageAlcoholByVolume = 10.1M },
                new Beers { BeerId = 5, BeerName = "Peroni", PercentageAlcoholByVolume = 16.1M },
                new Beers { BeerId = 6, BeerName = "xxxx", PercentageAlcoholByVolume = 36.5M },
            };
            return beerDetails;
        }
    }
}
