namespace BeerManagement.Repository.DatabaseContext
{
    public partial class LinkBreweryWithBeer
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public int BeerId { get; set; }
        public virtual Beers Beer { get; set; }
        public virtual Brewery Brewery { get; set; }
    }
}