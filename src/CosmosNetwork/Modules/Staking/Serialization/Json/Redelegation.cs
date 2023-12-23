using CosmosNetwork.Serialization;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Staking.Serialization.Json
{
    internal record RedelegationResponse(Redelegation Redelegation, RedelegationEntryBalance[] Entries)
    {
        public Staking.Redelegation ToModel()
        {
            return new Staking.Redelegation(this.Redelegation.DelegatorAddress, this.Redelegation.SourceValidatorAddress, this.Redelegation.DestinationValidatorAddress, this.Entries.Select(e => e.ToModel()).ToArray());
        }
    }

    internal record RedelegationEntryBalance(
        RedelegationEntry RedelegationEntry,
        DenomAmount Balance)
    {
        public Staking.RedelegationEntry ToModel()
        {
            return new Staking.RedelegationEntry(
                this.RedelegationEntry.CreationHeight,
                this.RedelegationEntry.CompletionTime,
                this.RedelegationEntry.InitialBalance,
                this.RedelegationEntry.SharesDestination,
                this.Balance.ToModel());
        }
    }

    internal record Redelegation(
        string DelegatorAddress,
        [property: JsonPropertyName("validator_src_address")] string SourceValidatorAddress,
        [property: JsonPropertyName("validator_dst_address")] string DestinationValidatorAddress,
        RedelegationEntry[] Entries);

    internal record RedelegationEntry(
        ulong CreationHeight,
        DateTime CompletionTime,
        decimal InitialBalance,
        [property: JsonPropertyName("shares_dst")] decimal SharesDestination);
}
