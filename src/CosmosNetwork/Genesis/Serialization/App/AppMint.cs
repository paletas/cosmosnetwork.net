using CosmosNetwork.Modules.Mint.Serialization;

namespace CosmosNetwork.Genesis.Serialization.App
{
    internal class AppMint
    {
        public Minter Minter { get; set; }

        public CosmosNetwork.Genesis.App.AppMint ToModel()
        {
            return new CosmosNetwork.Genesis.App.AppMint(
                this.Minter.ToModel());
        }
    }
}
