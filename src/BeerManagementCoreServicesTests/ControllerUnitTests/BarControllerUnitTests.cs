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
    public class BarControllerUnitTests
    {
        private readonly Mock<IBarRepository> _barRepository;

        private readonly BarController _barController;

        public BarControllerUnitTests()
        {
            _barRepository = new Mock<IBarRepository>();

            _barController =  new BarController(_barRepository.Object);
        }
        [Fact]
        public void GetBarDetailsById_Data_Found_Controller()
        {
            var barDetails = GetBarDetails();

            _barRepository.Setup(x => x.GetBarDetailsById(3)).Returns(barDetails[2]);

            var getbarDetailsById = _barController.GetBarDetailsById(3);

            Assert.NotNull(getbarDetailsById);
            Assert.True(barDetails[2].Equals(getbarDetailsById));
        }
        [Fact]
        public void GetBarDetailsById_No_Data_Found_Controller()
        {
            var barDetails = GetBarDetails();

            _barRepository.Setup(x => x.GetBarDetailsById(10)).Returns(barDetails.ElementAtOrDefault(9));

            var getbarDetailsById = _barController.GetBarDetailsById(10);

            Assert.Null(getbarDetailsById);
        }
        [Fact]
        public void GetAllBars_Data_Found_Controller()
        {
            var barDetails = GetBarDetails();

            _barRepository.Setup(x => x.GetAllBars()).Returns(barDetails);

            var getAllBarDetails = _barController.GetAllBars();

            Assert.NotNull(getAllBarDetails);
            Assert.Equal(getAllBarDetails, barDetails);
            Assert.True(getAllBarDetails.Count() > 4);
        }
        [Fact]
        public void UpdateBarDetails_Success_Controller()
        {
            var updateRecord = new Bars { BarId = 2, BarName = "Griffin Pub - New", BarAddress = "Leeds Met Hotel" };

            _barRepository.Setup(x => x.UpdateBarDetails(updateRecord)).Returns(Constants.updateOperation);

            var getResult = _barController.UpdateBarDetails(2, updateRecord);

            Assert.True(getResult == Constants.updateOperation);
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_BarName_Already_Exists_Controller()
        {
            var updateRecord = new Bars { BarId = 1, BarName = "London Bar & Pub", BarAddress = "Leeds Met Hotel" };

            _barRepository.Setup(x => x.UpdateBarDetails(updateRecord)).Returns(Constants.nameExists);

            var getResult = _barController.UpdateBarDetails(1, updateRecord);

            Assert.True(getResult == Constants.nameExists);
        }
        [Fact]
        public void UpdateBarDetails_ValidationFail_Id_Not_Found_Controller()
        {
            var updateRecord = new Bars { BarId = 10, BarName = "London Bar & Pub & Chips", BarAddress = "Leeds" };

            _barRepository.Setup(x => x.UpdateBarDetails(updateRecord)).Returns(Constants.notFound);

            var getResult = _barController.UpdateBarDetails(10, updateRecord);

            Assert.True(getResult == Constants.notFound);
        }
        [Fact]
        public void SaveNewBarDetails_Success_Controller()
        {
            var newRecord = new Bars { BarId = 6, BarName = "Hilife Bar & Pub", BarAddress = "Leeds High Street" };

            _barRepository.Setup(x => x.SaveNewBarDetails(newRecord)).Returns(Constants.createOperation);

            var getResult = _barController.SaveNewBarDetails(newRecord);

            Assert.True(getResult == Constants.createOperation);
        }
        [Fact]
        public void SaveNewBarDetails_ValidationFail_BarName_Already_Exists_Controller()
        {
            var newRecord = new Bars { BarId = 7, BarName = "United club Bar", BarAddress = "Leeds High Street" };

            _barRepository.Setup(x => x.SaveNewBarDetails(newRecord)).Returns(Constants.nameExists);

            var getResult = _barController.SaveNewBarDetails(newRecord);

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
