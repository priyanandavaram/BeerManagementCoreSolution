using BeerManagement.Models.DataModels;
using System.Collections.Generic;
using System.Text;

namespace BeerManagement.Services.Interfaces
{
    public interface IBeerService
    {
        BeerModel GetBeerDetailsById(int id);
        List<BeerModel> GetAllBeersByAlchoholPercentage(decimal gtAlcoholByVolume, decimal ltAlcoholByVolume);
        bool UpdateBeerDetails(BeerModel beerInfo, out string statusMessage);
        bool SaveNewBeerDetails(BeerModel beerInfo, out string statusMessage);
    }
}
