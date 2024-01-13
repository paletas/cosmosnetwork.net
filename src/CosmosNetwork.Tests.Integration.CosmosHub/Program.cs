using CosmosNetwork;
using CosmosNetwork.Tests.Integration;
using CosmosNetwork.Tests.Integration.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using IHost host = Host.CreateDefaultBuilder(args).Build();

ServiceCollection services = new ServiceCollection();
services.AddCosmosNetwork("https://rest.sentry-01.theta-testnet.polypore.xyz/", new CosmosApiOptions())
    .SetupCosmosHub("theta-testnet-001");

services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Information).AddConsole());

services.ConfigureIntegrationTests();

var serviceProvider = services.BuildServiceProvider();
serviceProvider.UseCosmosNetwork();

IEnumerable<IModule> modules = serviceProvider.GetServices<IModule>();

do
{
    Console.WriteLine($"[Q] Exit");
    int ix = 1;
    foreach (IModule module in modules)
    {
        Console.WriteLine($"[{ix++}] - {module.Name}");
    }

    Console.Write("Choose: ");
    string? key = Console.ReadLine();

    if (key is null || key.Equals("q", StringComparison.InvariantCultureIgnoreCase))
    {
        break;
    }
    else
    {
        if (int.TryParse(key, out int choice) && choice > 0 && choice <= modules.Count())
        {
            IModule module = modules.ElementAt(choice - 1);
            await module.Execute(CancellationToken.None);
        }
        else
        {
            Console.WriteLine("Wrong value! Try again.. Press to continue.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
while (true);
