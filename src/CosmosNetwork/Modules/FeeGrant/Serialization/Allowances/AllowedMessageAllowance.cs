namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    public record AllowedMessageAllowance(IAllowance Allowance, string[] AllowedMessages) : IAllowance
    {
        internal const string AllowanceType = "cosmos.feegrant.v1beta1.AllowedMsgAllowance";

        public string TypeUrl => AllowanceType;

        public FeeGrant.Allowances.IAllowance ToModel()
        {
            return new FeeGrant.Allowances.AllowedMessageAllowance(
                this.Allowance.ToModel(),
                this.AllowedMessages);
        }
    }
}
