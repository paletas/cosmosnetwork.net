using CosmosNetwork.Modules.Mint;

namespace CosmosNetwork.Genesis.App
{
    public record AppMint(Minter Minter)
    {
        internal Serialization.App.AppMint ToSerialization()
        {
            return new Serialization.App.AppMint
            {
                Minter = this.Minter.ToSerialization()
            };
        }
    }
}
