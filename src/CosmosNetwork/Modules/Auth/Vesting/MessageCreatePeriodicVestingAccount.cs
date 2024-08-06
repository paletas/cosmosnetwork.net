namespace CosmosNetwork.Modules.Auth.Vesting
{
    public record MessageCreatePeriodicVestingAccount(
        string MessageType,
        CosmosAddress FromAddress,
        CosmosAddress ToAddress,
        DateTime StartTime,
        VestingPeriod[] VestingPeriods) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.vesting.v1beta1.MsgCreatePeriodicVestingAccount";

        public override Serialization.Vesting.MessageCreatePeriodicVestingAccount ToSerialization()
        {
            return new Serialization.Vesting.MessageCreatePeriodicVestingAccount(
                this.FromAddress,
                this.ToAddress,
                this.StartTime,
                this.VestingPeriods.Select(x => x.ToSerialization()).ToArray());
        }
    }
}
