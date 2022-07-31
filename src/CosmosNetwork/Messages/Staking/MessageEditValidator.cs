namespace CosmosNetwork.Messages.Staking
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageEditValidator(
        CosmosAddress Validator,
        ulong? MinimumSelfDelegation,
        ValidatorDescription Description,
        decimal? ComissionRate) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgEditValidator";

        internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Staking.MessageEditValidator(
                Validator.Address, MinimumSelfDelegation,
                new Serialization.Messages.Staking.ValidatorDescription(
                    Description.Moniker,
                    Description.Identity,
                    Description.Details,
                    Description.Website,
                    Description.SecurityContact),
                ComissionRate
            );
        }
    }
}