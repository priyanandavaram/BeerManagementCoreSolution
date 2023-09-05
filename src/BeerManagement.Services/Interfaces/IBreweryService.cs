using BeerManagement.Models;
using System.Collections.Generic;
namespace BeerManagement.Services.Interfaces
{
    public interface IBreweryService
    {
        BreweryModel BreweryDetailsById(int id);
        List<BreweryModel> AllBreweries();
        bool BreweryDetailsUpdate(BreweryModel breweryInfo, out string statusMessage);
        bool NewBrewery(BreweryModel breweryInfo, out string statusMessage);
    }
}