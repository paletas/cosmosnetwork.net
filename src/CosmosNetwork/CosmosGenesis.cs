using CosmosNetwork.Genesis;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace CosmosNetwork
{
    public class CosmosGenesis([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
    {
        private readonly CosmosApiOptions _options = serviceProvider.GetRequiredKeyedService<CosmosApiOptions>(serviceKey);

        public CosmosGenesis(IServiceProvider serviceProvider)
            : this(CosmosNetworkConfigurator.DEFAULT_KEY, serviceProvider)
        { }

        public async Task<GenesisFile> LoadFromStream(Stream stream, CancellationToken cancellationToken)
        {
            using StreamReader reader = new StreamReader(stream);

            Genesis.Serialization.GenesisFile? genesisFile = await JsonSerializer.DeserializeAsync<Genesis.Serialization.GenesisFile>(stream, this._options.JsonSerializerOptions, cancellationToken)
                ?? throw new InvalidOperationException("unable to deserialize");

            return genesisFile.ToModel();
        }
    }
}
