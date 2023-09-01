using BeerManagementCoreServices.Database;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Interfaces
{
    public interface IBreweryRepository
    {
        Brewery GetBreweryDetailsById(int id);
        List<Brewery> GetAllBreweries();
        string UpdateBreweryDetails(Brewery updateBreweryDetails);
        string SaveNewBreweryDetails(Brewery createNewBrewery);
    }
}
