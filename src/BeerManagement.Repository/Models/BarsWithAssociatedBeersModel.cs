using BeerManagement.Models.DataModels;
using System.Collections.Generic;

namespace BeerManagement.Repository.Models
{
    public class BarsWithAssociatedBeersModel
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }
        public List<BeerModel> ListOfBeers { get; set; }
    }
}
