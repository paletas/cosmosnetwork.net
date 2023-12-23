namespace CosmosNetwork.Modules.Gov
{
    public record GovParams(GovVotingParams Voting, GovDepositParams Deposit, GovTallyParams Tally);

    public record GovVotingParams(TimeSpan VotingPeriod);

    public record GovDepositParams(Coin[] MinimumDeposit, TimeSpan MaximumDepositPeriod);

    public record GovTallyParams(decimal Quorum, decimal Threshold, decimal VetoThreshold);
}
