using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    public record RedelegationResponse
    {
        public Redelegation Redelegation { get; set; }

        public RedelegationEntryBalance[] Entries { get; set; }

        public Staking.Redelegation ToModel()
        {
            return new Staking.Redelegation(
                this.Redelegation.DelegatorAddress,
                this.Redelegation.ValidatorSrcAddress,
                this.Redelegation.ValidatorDstAddress,
                this.Entries.Select(e => e.ToModel()).ToArray());
        }
    }

    public record RedelegationEntryBalance
    {
        public RedelegationEntry RedelegationEntry { get; set; }

        public DenomAmount Balance { get; set; }

        public Staking.RedelegationEntry ToModel()
        {
            return new Staking.RedelegationEntry(
                this.RedelegationEntry.CreationHeight,
                this.RedelegationEntry.CompletionTime,
                this.RedelegationEntry.InitialBalance,
                this.RedelegationEntry.SharesDst);
        }
    }
}
