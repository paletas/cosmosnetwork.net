namespace CosmosNetwork.Modules.Staking.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageEditValidator(
        CosmosAddress Validator,
        ulong? MinimumSelfDelegation,
        ValidatorDescription Description,
        decimal? ComissionRate) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgEditValidator";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageEditValidator(
                this.Validator.Address, this.MinimumSelfDelegation,
                new Staking.Serialization.ValidatorDescription(
                    this.Description.Moniker,
                    this.Description.Identity,
                    this.Description.Details,
                    this.Description.Website.ToString(),
                    this.Description.SecurityContact),
                this.ComissionRate
            );
        }
    }
}