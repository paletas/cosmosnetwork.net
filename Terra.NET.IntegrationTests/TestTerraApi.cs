using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Terra.NET.IntegrationTests
{
    public class TestTerraApi : TerraApi
    {
        public TestTerraApi() 
            : base(new HttpClient() { BaseAddress = new Uri("https://bombay-lcd.terra.dev/") }, new LoggerFactory(), new TerraApiOptions(startingBlockHeightForTransactionSearch: 5900001))
        {
        }
    }
}
