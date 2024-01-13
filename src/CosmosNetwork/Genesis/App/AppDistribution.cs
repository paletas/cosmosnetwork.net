using CosmosNetwork.Modules.Distribution;

namespace CosmosNetwork.Genesis.App
{
    public record AppDistribution(
        Delegator[] Delegators,
        FeePool FeePool,
        Validator[] Validators,
        DistributionParams Params,
        CosmosAddress PreviousProposer)
    {
    }
}
