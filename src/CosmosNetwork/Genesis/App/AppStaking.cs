using CosmosNetwork.Modules.Staking;

namespace CosmosNetwork.Genesis.App
{
    public record AppStaking(
        Delegation[] Delegations,
        Redelegation[] Redelegations,
        Validator[] Validators,
        StakingParams Params,
        ulong LastTotalPower,
        bool Exported)
    {
    }
}
