namespace CosmosNetwork.Tendermint.Types
{
    public record ValidatorSet(
        Validator[] Validators,
        Validator Proposer,
        long TotalVotingPower)
    {
        public Serialization.ValidatorSet ToSerialization()
        {
            return new Serialization.ValidatorSet(
                this.Validators.Select(val => val.ToSerialization()).ToArray(),
                this.Proposer.ToSerialization(),
                this.TotalVotingPower);
        }
    }
}
