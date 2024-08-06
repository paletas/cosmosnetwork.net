namespace CosmosNetwork.Modules.Auth.Vesting
{
    public record MessageDonateAllVestingTokens(string MessageType, CosmosAddress FromAddress) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.vesting.v1beta1.MsgDonateAllVestingTokens";

        public override Serialization.Vesting.MessageDonateAllVestingTokens ToSerialization()
        {
            return new Serialization.Vesting.MessageDonateAllVestingTokens(this.FromAddress.Address);
        }
    }
}
