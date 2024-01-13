using CosmosNetwork.Genesis.App;

namespace CosmosNetwork.Genesis
{
    public record State(
        AppAuth Auth,
        AppBank Bank,
        AppCapability Capability,
        AppCrisis Crisis,
        AppDistribution Distribution,
        AppGenUtil GenUtil,
        AppGov Gov,
        AppMint Mint,
        AppSlashing Slashing,
        AppStaking Staking,
        AppTransfer Transfer);
}
