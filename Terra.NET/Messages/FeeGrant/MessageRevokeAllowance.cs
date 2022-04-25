namespace Terra.NET.Messages.FeeGrant
{
    public record MessageRevokeAllowance(TerraAddress Granter, TerraAddress Grantee)
        : Message(MessageTypeEnum.FeeGrantRevokeAllowance)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.FeeGrant.MessageRevokeAllowance(
                this.Granter.Address,
                this.Grantee.Address);
        }
    }
}