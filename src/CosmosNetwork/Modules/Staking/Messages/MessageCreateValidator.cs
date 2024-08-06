namespace CosmosNetwork.Modules.Staking.Messages
{
    public record MessageCreateValidator(
        string MessageType,
        CosmosAddress Delegator,
        CosmosAddress Validator,
        ulong MinimumSelfDelegation,
        ValidatorDescription Description,
        ValidatorCommissionRates Comission,
        Coin SelfDelegation,
        IPublicKey PublicKey) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.staking.v1beta1.MsgCreateValidator";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageCreateValidator(
                this.Delegator.Address, this.Validator.Address, this.MinimumSelfDelegation,
                new Staking.Serialization.ValidatorDescription(this.Description.Moniker, this.Description.Identity, this.Description.Details, this.Description.Website.ToString(), this.Description.SecurityContact),
                new Staking.Serialization.ValidatorCommissionRates(this.Comission.Rate, this.Comission.MaxRate, this.Comission.MaxRateChange),
                this.SelfDelegation.ToSerialization(),
                this.PublicKey.ToJson(),
                this.PublicKey.ToProto()
            );
        }
    }
}