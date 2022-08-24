using CosmosNetwork.Tendermint.Types.Serialization.Crypto;
using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record Validator(
        [property: ProtoMember(1, Name = "address")] byte[] Address,
        [property: ProtoMember(2, Name = "pub_key")] PublicKey PubKey,
        [property: ProtoMember(3, Name = "voting_power")] long VotingPower,
        [property: ProtoMember(4, Name = "proposer_priority")] long ProposerPriority)
    {
        public Types.Validator ToModel()
        {
            return new Types.Validator(
                this.Address,
                this.PubKey.ToModel(),
                this.VotingPower,
                this.ProposerPriority);
        }
    }
}
