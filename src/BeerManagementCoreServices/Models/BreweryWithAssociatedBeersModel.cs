using BeerManagementCoreServices.Database;
using System.Collections.Generic;

namespace BeerManagementCoreServices.Models
{
    public class BreweryWithAssociatedBeersModel
    {
        public int BreweryId { get; set; }
        public string BreweryName { get; set;}
        public List<Beers> ListOfBeers { get; set; }
    }
}
