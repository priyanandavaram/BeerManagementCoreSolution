using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BeerManagement.Repository.DatabaseContext
{
    public partial class Bars
    {
        public Bars()
        {
            LinkBarWithBeer = new HashSet<LinkBarWithBeer>();
        }

        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }

        public virtual ICollection<LinkBarWithBeer> LinkBarWithBeer { get; set; }
    }
}
