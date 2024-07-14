namespace CosmosNetwork.Modules.Auth.Vesting
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageCreatePermanentLockedAccount(
        string FromAddress,
        string ToAddress,
        Coin[] Amount) : Message
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
