using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Tendermint.Types;

namespace CosmosNetwork.Ibc.LightClients.Tendermint
{
    public record Header(
        SignedHeader SignedHeader,
        ValidatorSet ValidatorSet,
        Height TrustedHeight,
        ValidatorSet TrustedValidators) : IHeader
    {
        public Serialization.LightClients.IHeader ToSerialization()
        {
            return new Serialization.LightClients.Tendermint.Header(
                this.SignedHeader.ToSerialization(),
                this.ValidatorSet.ToSerialization(),
                this.TrustedHeight.ToSerialization(),
                this.TrustedValidators.ToSerialization());
        }
    }
}
