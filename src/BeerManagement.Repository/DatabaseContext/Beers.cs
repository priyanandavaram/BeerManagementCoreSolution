using System.Collections.Generic;
namespace BeerManagement.Repository.DatabaseContext
{
    public partial class Beers
    {
        public Beers()
        {
            LinkBarWithBeer = new HashSet<LinkBarWithBeer>();
            LinkBreweryWithBeer = new HashSet<LinkBreweryWithBeer>();
        }
        public int BeerId { get; set; }
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
        public virtual ICollection<LinkBarWithBeer> LinkBarWithBeer { get; set; }
        public virtual ICollection<LinkBreweryWithBeer> LinkBreweryWithBeer { get; set; }
    }
}