using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BeerManagement.Repository.DatabaseContext
{
    public partial class LinkBarWithBeer
    {
        public int Id { get; set; }
        public int BarId { get; set; }
        public int BeerId { get; set; }

        public virtual Bars Bar { get; set; }
        public virtual Beers Beer { get; set; }
    }
}
