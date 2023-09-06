using System.Collections.Generic;

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