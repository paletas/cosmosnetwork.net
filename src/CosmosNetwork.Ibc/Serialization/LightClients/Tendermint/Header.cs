using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Tendermint.Types.Serialization;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    internal record Header(
        SignedHeader SignedHeader,
        ValidatorSet ValidatorSet,
        Height TrustedHeight,
        ValidatorSet TrustedValidators) : IHeader
    {
        public const string AnyType = "/ibc.lightclients.tendermint.v1.Header";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IHeader ToModel()
        {
            return new Ibc.LightClients.Tendermint.Header(
                this.SignedHeader.ToModel(),
                this.ValidatorSet.ToModel(),
                this.TrustedHeight.ToModel(),
                this.TrustedValidators.ToModel());
        }
    }
}
