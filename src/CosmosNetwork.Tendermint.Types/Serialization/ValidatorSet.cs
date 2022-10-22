using ProtoBuf;

namespace CosmosNetwork.Tendermint.Types.Serialization
{
    [ProtoContract]
    public record ValidatorSet(
        [property: ProtoMember(1, Name = "validators")] Validator[] Validators,
        [property: ProtoMember(2, Name = "proposer")] Validator Proposer,
        [property: ProtoMember(3, Name = "total_voting_power")] long TotalVotingPower)
    {
        public Types.ValidatorSet ToModel()
        {
            return new Types.ValidatorSet(
                this.Validators.Select(val => val.ToModel()).ToArray(),
                this.Proposer.ToModel(),
                this.TotalVotingPower);
        }
    }
}
