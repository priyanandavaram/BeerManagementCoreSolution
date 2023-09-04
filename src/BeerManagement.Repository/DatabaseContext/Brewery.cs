using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

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
