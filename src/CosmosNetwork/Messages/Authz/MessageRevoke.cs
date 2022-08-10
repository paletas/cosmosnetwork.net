namespace CosmosNetwork.Messages.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRevoke(
        CosmosAddress Granter,
        CosmosAddress Grantee,
        string MessageTypeUrl) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgRevoke";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Authz.MessageRevoke(Granter.Address, Grantee.Address, MessageTypeUrl);
        }
    }
}