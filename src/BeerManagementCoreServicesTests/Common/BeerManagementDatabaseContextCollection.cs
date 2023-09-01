using Xunit;

namespace BeerManagementCoreServicesTests.Common
{
    [CollectionDefinition("BeerManagementDatabaseContextCollection")]
    public class BeerManagementDatabaseContextCollection : ICollectionFixture<BeerManagementDatabaseContextFixture>
    {
    }
}
