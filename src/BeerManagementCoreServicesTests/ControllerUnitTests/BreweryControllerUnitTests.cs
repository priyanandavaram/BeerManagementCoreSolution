using BeerManagementCoreServices.Controllers;
using BeerManagementCoreServices.Interfaces;
using BeerManagementCoreServices.Database;
using BeerManagementCoreServices.Common;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace BeerManagementCoreServicesTests.ControllerUnitTests
{
    public class BreweryControllerUnitTests
    {
        private readonly Mock<IBreweryRepository> _breweryRepository;
        
        private readonly BreweryController _breweryController;

        public BreweryControllerUnitTests()
        {
            _breweryRepository = new Mock<IBreweryRepository>();

            _breweryController = new BreweryController(_breweryRepository.Object);
        }
        [Fact]
        public void GetBreweryDetailsById_Data_Found_Controller()
        {
            var breweryDetails = GetBreweryDetails();

            _breweryRepository.Setup(x => x.GetBreweryDetailsById(3)).Returns(breweryDetails[2]);

            var getbreweryDetailsById = _breweryController.GetBreweryDetailsById(3);

            Assert.NotNull(getbreweryDetailsById);
            Assert.True(breweryDetails[2].Equals(getbreweryDetailsById));
        }
        [Fact]
        public void GetBreweryDetailsById_No_Data_Found_Controller()
        {
            var breweryDetails = GetBreweryDetails();

            _breweryRepository.Setup(x => x.GetBreweryDetailsById(10)).Returns(breweryDetails.ElementAtOrDefault(9));

            var getbreweryDetailsById = _breweryController.GetBreweryDetailsById(10);

            Assert.Null(getbreweryDetailsById);
        }
        [Fact]
        public void GetAllBreweries_Data_Found_Controller()
        {
            var breweryDetails = GetBreweryDetails();

            _breweryRepository.Setup(x => x.GetAllBreweries()).Returns(breweryDetails);

            var getAllBreweryDetails = _breweryController.GetAllBreweries();

            Assert.NotNull(getAllBreweryDetails);
            Assert.Equal(getAllBreweryDetails,breweryDetails);
        }
        [Fact]
        public void UpdateBreweryDetails_Success_Controller()
        {
            var updateRecord = new Brewery { BreweryId = 2, BreweryName = "United Beverages-Mosco-Bar" };

            _breweryRepository.Setup(x => x.UpdateBreweryDetails(updateRecord)).Returns(Constants.updateOperation);

            var getResult = _breweryController.UpdateBreweryDetails(2, updateRecord);

            Assert.True(getResult == Constants.updateOperation);
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_BreweryName_Already_Exists_Controller()
        {
            var updateRecord = new Brewery { BreweryId = 3, BreweryName = "United Beverages-Dallas" };

            _breweryRepository.Setup(x => x.UpdateBreweryDetails(updateRecord)).Returns(Constants.nameExists);

            var getResult = _breweryController.UpdateBreweryDetails(3, updateRecord);

            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBreweryDetails_ValidationFail_Id_Not_Found_Controller()
        {
            var updateRecord = new Brewery { BreweryId = 10, BreweryName = "test" };

            _breweryRepository.Setup(x => x.UpdateBreweryDetails(updateRecord)).Returns(Constants.notFound);

            var getResult = _breweryController.UpdateBreweryDetails(10, updateRecord);

            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBreweryDetails_Success_Controller()
        {
            var newRecord = new Brewery { BreweryId = 6, BreweryName = "Mexical Beverage" };

            _breweryRepository.Setup(x => x.SaveNewBreweryDetails(newRecord)).Returns(Constants.createOperation);

            var getResult = _breweryController.SaveNewBreweryDetails(newRecord);

            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBreweryDetails_ValidationFail_BreweryName_Exists_Controller()
        {
            var newRecord = new Brewery { BreweryId = 7, BreweryName = "United Beverages-Dallas" };

            _breweryRepository.Setup(x => x.SaveNewBreweryDetails(newRecord)).Returns(Constants.nameExists);

            var breweryController = new BreweryController(_breweryRepository.Object);

            var getResult = breweryController.SaveNewBreweryDetails(newRecord);

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
