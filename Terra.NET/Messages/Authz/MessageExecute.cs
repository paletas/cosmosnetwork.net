namespace Terra.NET.Messages.Authz
{
    public record MessageExecute(TerraAddress Grantee, Message[] Messages)
        : Message(MessageTypeEnum.AuthzExecute)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Authz.MessageExecute(this.Grantee.Address, this.Messages.Select(msg => msg.ToJson()).ToArray());
        }
    }
}