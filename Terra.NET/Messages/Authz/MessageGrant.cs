namespace Terra.NET.Messages.Authz
{
    public record MessageGrant(TerraAddress Granter, TerraAddress Grantee)
        : Message(MessageTypeEnum.AuthzGrant)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Authz.MessageGrant(this.Granter.Address, this.Grantee.Address);
        }
    }
}