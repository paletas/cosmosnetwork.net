namespace CosmosNetwork.Modules.Staking
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
                Validator.Address, MinimumSelfDelegation,
                new Serialization.ValidatorDescription(
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