namespace Terra.NET.Messages.Authz
{
    public record MessageRevoke(TerraAddress Granter, TerraAddress Grantee, string MessageTypeUrl)
        : Message(MessageTypeEnum.AuthzRevoke)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Authz.MessageRevoke(this.Granter.Address, this.Grantee.Address, this.MessageTypeUrl);
        }
    }
}