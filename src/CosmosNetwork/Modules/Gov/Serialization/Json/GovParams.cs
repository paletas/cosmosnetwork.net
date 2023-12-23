using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization.Json
{
    internal record GovParams(GovVotingParams VotingParams, GovDepositParams DepositParams, GovTallyParams TallyParams)
    {
        public Gov.GovParams ToModel()
        {
            return new Gov.GovParams(
                this.VotingParams.ToModel(), 
                this.DepositParams.ToModel(), 
                this.TallyParams.ToModel());
        }
    }

    internal record GovVotingParams(TimeSpan VotingPeriod)
    {
        public Gov.GovVotingParams ToModel()
        {
            return new Gov.GovVotingParams(this.VotingPeriod);
        }
    }

    internal record GovDepositParams(DenomAmount[] MinDeposit, TimeSpan MaxDepositPeriod)
    {
        public Gov.GovDepositParams ToModel()
        {
            return new Gov.GovDepositParams(this.MinDeposit.Select(amt => amt.ToModel()).ToArray(), this.MaxDepositPeriod);
        }
    }

    internal record GovTallyParams(decimal Quorum, decimal Threshold, decimal VetoThreshold)
    {
        public Gov.GovTallyParams ToModel()
        {
            return new Gov.GovTallyParams(this.Quorum, this.Threshold, this.VetoThreshold);
        }
    }
}
