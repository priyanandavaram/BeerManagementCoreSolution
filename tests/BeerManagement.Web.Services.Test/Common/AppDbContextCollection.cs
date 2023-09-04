using Xunit;

namespace BeerManagement.Web.Services.Test.Common
{
    [CollectionDefinition("AppDbContextCollection")]
    public class AppDbContextCollection : ICollectionFixture<AppDbContextFixture>
    {
    }
}
