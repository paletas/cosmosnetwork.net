namespace CosmosNetwork.Modules.Staking
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageCreateValidator(
        CosmosAddress Delegator,
        CosmosAddress Validator,
        ulong MinimumSelfDelegation,
        ValidatorDescription Description,
        ValidatorComission Comission,
        Coin SelfDelegation,
        SignatureKey PublicKey) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgCreateValidator";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageCreateValidator(
                Delegator.Address, Validator.Address, MinimumSelfDelegation,
                new Serialization.ValidatorDescription(Description.Moniker, Description.Identity, Description.Details, Description.Website, Description.SecurityContact),
                new Serialization.ValidatorComission(Comission.Rate, Comission.MaxRate, Comission.MaxRateChange),
                new CosmosNetwork.Serialization.DenomAmount(SelfDelegation.Denom, SelfDelegation.Amount),
                PublicKey.ToSerialization()
            );
        }
    }

    public record ValidatorDescription(string Moniker, string Identity, string Details, string Website, string SecurityContact);

    public record ValidatorComission(decimal Rate, decimal MaxRate, decimal MaxRateChange);
}