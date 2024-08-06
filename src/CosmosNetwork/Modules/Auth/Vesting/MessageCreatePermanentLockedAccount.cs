namespace CosmosNetwork.Modules.Auth.Vesting
{
    public record MessageCreatePermanentLockedAccount(
        string MessageType,
        CosmosAddress FromAddress,
        CosmosAddress ToAddress,
        Coin[] Amount) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.vesting.v1beta1.MsgCreatePermanentLockedAccount";

        public override Serialization.Vesting.MessageCreatePermanentLockedAccount ToSerialization()
        {
            return new Serialization.Vesting.MessageCreatePermanentLockedAccount(
                this.FromAddress,
                this.ToAddress,
                this.Amount.Select(x => x.ToSerialization()).ToArray());
        }
    }
}
