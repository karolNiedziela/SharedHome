using Xunit;

namespace SharedHome.IntegrationTests
{
    [CollectionDefinition("Shared test collection")]
    public class SharedTestCollection : ICollectionFixture<CustomWebApplicationFactory>
    {
    }
}
