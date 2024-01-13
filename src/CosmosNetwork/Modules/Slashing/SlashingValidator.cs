namespace CosmosNetwork.Modules.Slashing
{
    public record SlashingValidator(
        CosmosAddress ValidatorAddress,
        SigningInfo SigningInfo,
        SlashingBlock[] Blocks)
    {
    }
}
