namespace CosmosNetwork.Modules.Auth.Vesting
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageDonateAllVestingTokens(CosmosAddress FromAddress) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.vesting.v1beta1.MsgDonateAllVestingTokens";

        public override Serialization.Vesting.MessageDonateAllVestingTokens ToSerialization()
        {
            return new Serialization.Vesting.MessageDonateAllVestingTokens(this.FromAddress.Address);
        }
    }
}
