using CosmosNetwork.Genesis;

namespace CosmosNetwork.Tests.Integration.Modules.Genesis
{
    internal class GenesisModule(CosmosGenesis cosmosGenesis) : IModule
    {
        private readonly CosmosGenesis _cosmosGenesis = cosmosGenesis;

        public string Name => "Genesis";

        public async Task Execute(CancellationToken cancellationToken)
        {
            do
            {
                Console.WriteLine("Enter a path to the Genesis File:");
                string? path = Console.ReadLine();

                if (path is null)
                {
                    Console.WriteLine("invalid path!");
                    break;
                }

                if (!File.Exists(path))
                {
                    Console.WriteLine("File does not exist!");
                    break;
                }

                FileStream fileStream = File.OpenRead(path);
                GenesisFile genesisFile = await this._cosmosGenesis.LoadFromStream(fileStream, cancellationToken);

                Console.WriteLine($"Height: {genesisFile.InitialHeight}");
            }
            while (true);

        }
    }
}
