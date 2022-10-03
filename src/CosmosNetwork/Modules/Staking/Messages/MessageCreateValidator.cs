namespace CosmosNetwork.Modules.Staking.Messages
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageCreateValidator(
        CosmosAddress Delegator,
        CosmosAddress Validator,
        ulong MinimumSelfDelegation,
        ValidatorDescription Description,
        ValidatorCommissionRates Comission,
        Coin SelfDelegation,
        SignatureKey PublicKey) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgCreateValidator";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageCreateValidator(
                Delegator.Address, Validator.Address, MinimumSelfDelegation,
                new Staking.Serialization.ValidatorDescription(Description.Moniker, Description.Identity, Description.Details, Description.Website.ToString(), Description.SecurityContact),
                new Staking.Serialization.ValidatorCommissionRates(Comission.Rate, Comission.MaxRate, Comission.MaxRateChange),
                SelfDelegation.ToSerialization(),
                PublicKey.ToJson(),
                PublicKey.ToProto()
            );
        }
    }
}