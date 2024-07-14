namespace CosmosNetwork.Modules.Auth.Vesting
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageCreatePeriodicVestingAccount(
        string FromAddress,
        string ToAddress,
        DateTime StartTime,
        VestingPeriod[] VestingPeriods) : Message
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
