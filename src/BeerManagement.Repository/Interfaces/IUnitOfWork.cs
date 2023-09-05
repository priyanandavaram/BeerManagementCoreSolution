namespace BeerManagement.Repository.Interfaces
{
    public interface IUnitOfWork<entity>
    {
        IBarAndBeerRepository BarAndBeerRepository { get; }
        IBreweryAndBeerRepository BreweryAndBeerRepository { get; }
        IGenericRepository<entity> GenericRepository { get; }
        IBeerRepository BeerRepository { get; }
    }
}