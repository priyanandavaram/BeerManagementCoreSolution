using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BeerManagementCoreServices.Database
{
    public partial class Beers
    {
        public int BeerId { get; set; }
        public string BeerName { get; set; }
        public decimal PercentageAlcoholByVolume { get; set; }
    }
}
