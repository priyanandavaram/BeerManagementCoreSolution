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