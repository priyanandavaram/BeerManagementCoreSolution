using BeerManagement.Models.DataModels;
using System.Collections.Generic;
using System.Text;

namespace BeerManagement.Services.Interfaces
{
    public interface IBreweryService
    {
        BreweryModel GetBreweryDetailsById(int id);
        List<BreweryModel> GetAllBreweries();
        bool UpdateBreweryDetails(BreweryModel breweryInfo, out string statusMessage);
        bool SaveNewBreweryDetails(BreweryModel breweryInfo, out string statusMessage);
    }
}
