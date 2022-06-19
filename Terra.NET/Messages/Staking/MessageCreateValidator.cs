namespace Terra.NET.Messages.Staking
{
    public record MessageCreateValidator(
        TerraAddress Delegator, TerraAddress Validator, ulong MinimumSelfDelegation,
        ValidatorDescription Description, ValidatorComission Comission, Coin SelfDelegation,
        SignatureKey PublicKey
    ) : Message(MessageTypeEnum.StakingCreateValidator)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Staking.MessageCreateValidator(
                this.Delegator.Address, this.Validator.Address, this.MinimumSelfDelegation,
                new API.Serialization.Json.Messages.Staking.ValidatorDescription(this.Description.Moniker, this.Description.Identity, this.Description.Details, this.Description.Website, this.Description.SecurityContact),
                new API.Serialization.Json.Messages.Staking.ValidatorComission(this.Comission.Rate, this.Comission.MaxRate, this.Comission.MaxRateChange),
                new API.Serialization.Json.DenomAmount(this.SelfDelegation.Denom, this.SelfDelegation.Amount),
                this.PublicKey.ToJson()
            );
        }
    }

    public record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    public record ValidatorComission(decimal Rate, decimal MaxRate, decimal MaxRateChange);
}