using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Tendermint.Types.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record Header(
        [property: ProtoMember(1, Name = "signed_header")] SignedHeader SignedHeader,
        [property: ProtoMember(2, Name = "validator_set")] ValidatorSet ValidatorSet,
        [property: ProtoMember(3, Name = "trusted_height")] Height TrustedHeight,
        [property: ProtoMember(4, Name = "trusted_validators")] ValidatorSet TrustedValidators) : IHeader
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
