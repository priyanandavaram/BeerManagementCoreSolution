using BeerManagementCoreServices.Database;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Interfaces
{
    public interface IBarRepository
    {
        Bars GetBarDetailsById(int id);
        List<Bars> GetAllBars();
        string UpdateBarDetails(Bars updateBarDetails);
        string SaveNewBarDetails(Bars createNewBar);
    }
}
