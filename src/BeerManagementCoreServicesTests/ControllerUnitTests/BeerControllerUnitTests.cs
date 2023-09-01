using BeerManagementCoreServices.Controllers;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests
{
    public class BeerControllerUnitTests
    {
        private readonly Mock<IBeerRepository> _beerRepository;

        private readonly BeerController _beerController;

        public BeerControllerUnitTests()
        {
            _beerRepository = new Mock<IBeerRepository>();

            _beerController = new BeerController(_beerRepository.Object);
        }
        [Fact]
        public void GetBeerDetailsById_Data_Found_Controller()
        {
            var beerDetails = GetBeerDetails();

            _beerRepository.Setup(x => x.GetBeerDetailsById(4)).Returns(beerDetails[3]);

            var getbeerDetailsById = _beerController.GetBeerDetailsById(4);

            Assert.NotNull(getbeerDetailsById);           
            Assert.True(beerDetails[3].Equals(getbeerDetailsById));
        }
        [Fact]
        public void GetBeerDetailsById_No_Data_Found_Controller()
        {
            var beerDetails = GetBeerDetails();

            _beerRepository.Setup(x => x.GetBeerDetailsById(10)).Returns(beerDetails.ElementAtOrDefault(9));

            var getbeerDetailsById = _beerController.GetBeerDetailsById(10);

            Assert.Null(getbeerDetailsById);
        }
        [Fact]
        public void GetAllBeersByAlchoholPercentage_Controller()
        {
            var beerDetails = GetBeerDetails();

            var filteredBeerDetails = beerDetails.Where(x => x.PercentageAlcoholByVolume > 10 && x.PercentageAlcoholByVolume < 40).ToList();

            _beerRepository.Setup(x => x.GetAllBeersByAlchoholPercentage(10, 40)).Returns(filteredBeerDetails);

            var getBeerDetailsByAlcoholPercentage = _beerController.GetAllBeersByAlchoholPercentage(10, 40);

            Assert.NotNull(getBeerDetailsByAlcoholPercentage);
            Assert.Equal(getBeerDetailsByAlcoholPercentage, filteredBeerDetails);
            Assert.True(getBeerDetailsByAlcoholPercentage.Count() < beerDetails.Count());
        }
        [Fact]
        public void GetAllBeersByAlchoholPercentage_With_Optional_Values_Controller()
        {
            var beerDetails = GetBeerDetails();

            decimal gtAlcoholByVolume = 0;
            decimal ltAlcoholByVolume = 0;

            var filteredBeerDetails = beerDetails.Where(B => ((gtAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume > gtAlcoholByVolume)) || gtAlcoholByVolume == 0) && ((ltAlcoholByVolume != 0 && (B.PercentageAlcoholByVolume < ltAlcoholByVolume)) || (ltAlcoholByVolume == 0))).ToList();

            _beerRepository.Setup(x => x.GetAllBeersByAlchoholPercentage(0, 0)).Returns(filteredBeerDetails);

            var getBeerDetailsByAlcoholPercentage = _beerController.GetAllBeersByAlchoholPercentage(0, 0);

            Assert.NotNull(getBeerDetailsByAlcoholPercentage);
            Assert.Equal(getBeerDetailsByAlcoholPercentage, filteredBeerDetails);
            Assert.True(getBeerDetailsByAlcoholPercentage.Count() == beerDetails.Count());
        }
        [Fact]
        public void UpdateBeerDetails_Success_Controller()
        {
            var updateRecord = new Beers { BeerId = 2, BeerName = "Stella - Gold", PercentageAlcoholByVolume = 4.08M };

            _beerRepository.Setup(x => x.UpdateBeerDetails(updateRecord)).Returns(Constants.updateOperation);

            var getResult = _beerController.UpdateBeerDetails(2, updateRecord);

            Assert.True(getResult == Constants.updateOperation);          
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_BeerName_Already_Exists_Controller()
        {
            var updateRecord = new Beers { BeerId = 3, BeerName = "Kingfisher", PercentageAlcoholByVolume = 9.9M };

            _beerRepository.Setup(x => x.UpdateBeerDetails(updateRecord)).Returns(Constants.nameExists);

            var getResult = _beerController.UpdateBeerDetails(3, updateRecord);

            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBeerDetails_ValidationFail_Id_NotFound_Controller()
        {
            var updateRecord = new Beers { BeerId = 10, BeerName = "Test", PercentageAlcoholByVolume = 0.0M };

            _beerRepository.Setup(x => x.UpdateBeerDetails(updateRecord)).Returns(Constants.notFound);

            var getResult = _beerController.UpdateBeerDetails(10, updateRecord);

            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBeerDetails_Success_Controller()
        {
            var newRecord = new Beers { BeerId = 7, BeerName = "Merina", PercentageAlcoholByVolume = 0.08M };

            _beerRepository.Setup(x => x.SaveNewBeerDetails(newRecord)).Returns(Constants.createOperation);

            var getResult = _beerController.SaveNewBeerDetails(newRecord);

            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBeerDetails_ValidationFail_BeerName_Exists_Controller()
        {
            var newRecord = new Beers { BeerId = 8, BeerName = "Budweiser", PercentageAlcoholByVolume = 0.08M };

            _beerRepository.Setup(x => x.SaveNewBeerDetails(newRecord)).Returns(Constants.nameExists);

            var getResult = _beerController.SaveNewBeerDetails(newRecord);

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


