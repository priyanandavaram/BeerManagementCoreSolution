using BeerManagement.Models.DataModels;
using System.Collections.Generic;

namespace BeerManagement.Services.Interfaces
{
    public interface IBarService
    {
        BarModel GetBarDetailsById(int id);
        List<BarModel> GetAllBars();
        bool UpdateBarDetails(BarModel barInfo, out string statusMessage);
        bool SaveNewBarDetails(BarModel barInfo, out string statusMessage);
    }
}
