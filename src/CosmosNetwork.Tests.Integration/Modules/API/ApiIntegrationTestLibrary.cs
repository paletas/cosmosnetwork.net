using CosmosNetwork.Tests.Integration.Modules.API.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CosmosNetwork.Tests.Integration.Modules.API
{
    public class ApiIntegrationTestLibrary
    {
        private readonly List<IApiIntegrationTest> _integrationTests;

        public ApiIntegrationTestLibrary(IServiceProvider serviceProvider)
        {
            _integrationTests = new List<IApiIntegrationTest>
            {
                new CrawlerTest(serviceProvider.GetRequiredService<ILogger<CrawlerTest>>(), serviceProvider.GetRequiredService<CosmosApi>()),
                new EstimationTest(serviceProvider.GetRequiredService<ILogger<EstimationTest>>(), serviceProvider.GetRequiredService<CosmosApi>())
            };
        }

        public IEnumerable<IApiIntegrationTest> GetTests()
        {
            return _integrationTests;
        }
    }
}
