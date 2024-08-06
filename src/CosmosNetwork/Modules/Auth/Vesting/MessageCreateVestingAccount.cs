namespace CosmosNetwork.Modules.Auth.Vesting
{
    public record MessageCreateVestingAccount(
        string MessageType,
        CosmosAddress FromAddress,
        CosmosAddress ToAddress,
        Coin[] Amount,
        DateTime StartTime,
        DateTime EndTime,
        bool Delayed) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.vesting.v1beta1.MsgCreateVestingAccount";

        public override Serialization.Vesting.MessageCreateVestingAccount ToSerialization()
        {
            return new Serialization.Vesting.MessageCreateVestingAccount(
                this.FromAddress,
                this.ToAddress,
                this.Amount.Select(x => x.ToSerialization()).ToArray(),
                this.StartTime,
                this.EndTime,
                this.Delayed);
        }
    }
}
