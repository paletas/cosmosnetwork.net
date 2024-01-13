using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Tests.Integration.Modules.API
{
    internal class ApiModule(IServiceProvider serviceProvider) : IModule
    {
        public string Name => "API Tests";

        public async Task Execute(CancellationToken cancellation)
        {
            var integrationTestLibrary = serviceProvider.GetRequiredService<ApiIntegrationTestLibrary>();

            var integrationTests = integrationTestLibrary.GetTests();
            var integrationTestsQuantity = integrationTests.Count();

            CosmosApi cosmosApi = serviceProvider.GetRequiredService<CosmosApi>();
            Console.WriteLine("Setup wallet, mnemonic key:");
            string? mnemonicKey = Console.ReadLine();

            while (mnemonicKey is null)
            {
                Console.Clear();
                Console.WriteLine("Setup wallet, mnemonic key:");
                mnemonicKey = Console.ReadLine();
            }

            IWallet wallet = await cosmosApi.Wallet.GetWallet(mnemonicKey, new CosmosNetwork.Keys.Sources.MnemonicKeyOptions());

            do
            {
                Console.WriteLine("Integration Tests Available");
                int ix = 1;
                foreach (var test in integrationTests)
                {
                    Console.WriteLine($"[{ix++}] {test.Name}");
                }

                IApiIntegrationTest? testChoosen = null;
                bool testChoiceStatus = false;
                do
                {
                    Console.Write("Choose a test: ");
                    var choosenTestNumberString = Console.ReadLine();
                    int choosenTestNumber;

                    if (int.TryParse(choosenTestNumberString, out choosenTestNumber))
                    {
                        if (choosenTestNumber <= 0 || choosenTestNumber > integrationTestsQuantity)
                        {
                            Console.WriteLine($"There's no such test, pick a number between 1 and {integrationTestsQuantity}");
                        }
                        else
                        {
                            testChoosen = integrationTests.ElementAt(choosenTestNumber - 1);
                            testChoiceStatus = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input! Numbers only.");
                    }
                }
                while (testChoiceStatus == false);

                if (testChoosen is null) throw new InvalidOperationException();
                await testChoosen.Execute(wallet);

                Console.ReadKey();

                Console.Clear();
            }
            while (true);
        }
    }
}
