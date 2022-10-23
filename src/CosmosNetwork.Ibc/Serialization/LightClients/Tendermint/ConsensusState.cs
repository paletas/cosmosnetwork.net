using CosmosNetwork.Ibc.Serialization.Core.Commitment;
using ProtoBuf;
using ProtoBuf.WellKnownTypes;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record ConsensusState(
        [property: ProtoMember(1, Name = "timestamp")] Timestamp Timestamp,
        [property: ProtoMember(2, Name = "root")] MerkleRoot Root,
        [property: ProtoMember(3, Name = "next_validators_hash")] byte[] NextValidatorsHash) : IConsensusState
    {
        public const string AnyType = "/ibc.lightclients.tendermint.v1.ConsensusState";

        public string TypeUrl => AnyType;

        public Ibc.LightClients.IConsensusState ToModel()
        {
            return new Ibc.LightClients.Tendermint.ConsensusState(
                this.Timestamp,
                this.Root.ToModel(),
                this.NextValidatorsHash);
        }
    }
}
