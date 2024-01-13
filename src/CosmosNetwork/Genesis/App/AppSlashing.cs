using CosmosNetwork.Modules.Slashing;

namespace CosmosNetwork.Genesis.App
{
    public record AppSlashing(SlashingValidator[] Validators, SlashingParams Params)
    {
    }
}
