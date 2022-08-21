namespace CosmosNetwork.Modules.FeeGrant.Allowances
{
    public record AllowedMessageAllowance(IAllowance Allowance, string[] AllowedMessages)
        : IAllowance
    {
        public const string AllowanceType = "/cosmos.feegrant.v1beta1.AllowedMsgAllowance";

        public Serialization.Allowances.IAllowance ToSerialization()
        {
            return new Serialization.Allowances.AllowedMessageAllowance(
                Allowance.ToSerialization(),
                AllowedMessages);
        }
    }
}