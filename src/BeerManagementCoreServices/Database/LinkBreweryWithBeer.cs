using Newtonsoft.Json;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BeerManagementCoreServices.Database
{
    public partial class LinkBreweryWithBeer
    {
        public int BreweryId { get; set; }
        public int BeerId { get; set; }
        public virtual Beers Beer { get; set; }       
        public virtual Brewery Brewery { get; set; }
    }
}
