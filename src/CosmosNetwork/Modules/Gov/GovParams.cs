namespace CosmosNetwork.Modules.Gov
{
    public record GovParams(GovVotingParams Voting, GovDepositParams Deposit, GovTallyParams Tally)
    {
        internal Serialization.GovParams ToSerialization()
        {
            return new Serialization.GovParams(
                this.Voting.ToSerialization(),
                this.Deposit.ToSerialization(),
                this.Tally.ToSerialization());
        }
    }

    public record GovVotingParams(TimeSpan VotingPeriod)
    {
        internal Serialization.GovVotingParams ToSerialization()
        {
            return new Serialization.GovVotingParams(this.VotingPeriod);
        }
    }

    public record GovDepositParams(Coin[] MinimumDeposit, TimeSpan MaximumDepositPeriod)
    {
        internal Serialization.GovDepositParams ToSerialization()
        {
            return new Serialization.GovDepositParams(this.MinimumDeposit.Select(acc => acc.ToSerialization()).ToArray(), this.MaximumDepositPeriod);
        }
    }

    public record GovTallyParams(decimal Quorum, decimal Threshold, decimal VetoThreshold)
    {
        internal Serialization.GovTallyParams ToSerialization()
        {
            return new Serialization.GovTallyParams(this.Quorum, this.Threshold, this.VetoThreshold);
        }
    }
}
