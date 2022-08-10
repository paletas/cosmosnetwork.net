namespace CosmosNetwork.Messages.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageExecute(
        CosmosAddress Grantee,
        Message[] Messages) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgExecAuthorized";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Authz.MessageExecute(Grantee.Address, Messages.Select(msg => msg.ToJson()).ToArray());
        }
    }
}