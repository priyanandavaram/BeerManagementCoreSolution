using BeerManagement.Models;
using System.Collections.Generic;

namespace BeerManagement.Repository.Models
{
    public class BreweryWithAssociatedBeersModel
    {
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public List<BeerModel> ListOfBeers { get; set; }
    }
}