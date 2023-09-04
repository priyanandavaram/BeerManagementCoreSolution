using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
