using System.Collections.Generic;

namespace BeerManagement.Repository.DatabaseContext
{
    public partial class Brewery
    {
        public Brewery()
        {
            LinkBreweryWithBeer = new HashSet<LinkBreweryWithBeer>();
        }
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public virtual ICollection<LinkBreweryWithBeer> LinkBreweryWithBeer { get; set; }
    }
}