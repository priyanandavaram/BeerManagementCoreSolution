using BeerManagementCoreServices.Database;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Models
{
    public class BarWithAssociatedBeersModel
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }
        public List<Beers> ListOfBeers { get; set; }
    }
}
