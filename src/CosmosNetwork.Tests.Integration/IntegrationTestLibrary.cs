using CosmosNetwork.Tests.Integration.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.Tests.Integration
{
    public class IntegrationTestLibrary
    {
        private readonly List<IIntegrationTest> _integrationTests;

        public IntegrationTestLibrary(IServiceProvider serviceProvider)
        {
            _integrationTests = new List<IIntegrationTest>
            {
                new CrawlerTest(serviceProvider.GetRequiredService<ILogger<CrawlerTest>>(), serviceProvider.GetRequiredService<CosmosApi>()),
                new EstimationTest(serviceProvider.GetRequiredService<ILogger<EstimationTest>>(), serviceProvider.GetRequiredService<CosmosApi>())
            };
        }

        public IEnumerable<IIntegrationTest> GetTests()
        {
            return _integrationTests;
        }
    }
}
