using BeerManagement.Models;
using System.Collections.Generic;
namespace BeerManagement.Services.Interfaces
{
    public interface IBarService
    {
        BarModel BarDetailsById(int id);
        List<BarModel> AllBars();
        bool BarDetailsUpdate(BarModel barInfo, out string statusMessage);
        bool NewBar(BarModel barInfo, out string statusMessage);
    }
}