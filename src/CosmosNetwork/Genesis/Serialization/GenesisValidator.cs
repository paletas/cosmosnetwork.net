using CosmosNetwork.Serialization.Json;

namespace CosmosNetwork.Genesis.Serialization
{
    public class GenesisValidator
    {
        public string Address { get; set; }

        public PublicKey PubKey { get; set; }

        public string Power { get; set; }

        public string Name { get; set; }
    }
}
