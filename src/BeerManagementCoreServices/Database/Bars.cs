using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BeerManagementCoreServices.Database
{
    public partial class Bars
    {
        public int BarId { get; set; }
        public string BarName { get; set; }
        public string BarAddress { get; set; }
    }
}
