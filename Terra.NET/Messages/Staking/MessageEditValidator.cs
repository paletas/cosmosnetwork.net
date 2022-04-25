namespace Terra.NET.Messages.Staking
{
    public record MessageEditValidator(
        TerraAddress Validator, ulong MinimumSelfDelegation,
        ValidatorDescription Description, decimal ComissionRate
    ) : Message(MessageTypeEnum.StakingEditValidator)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Staking.MessageEditValidator(
                this.Validator.Address, this.MinimumSelfDelegation,
                new API.Serialization.Json.Messages.Staking.ValidatorDescription(
                    this.Description.Moniker,
                    this.Description.Identity,
                    this.Description.Details,
                    this.Description.Website,
                    this.Description.SecurityContact),
                this.ComissionRate
            );
        }
    }
}