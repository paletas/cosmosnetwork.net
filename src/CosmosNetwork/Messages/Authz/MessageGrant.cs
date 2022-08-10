namespace CosmosNetwork.Messages.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageGrant(
        CosmosAddress Granter,
        CosmosAddress Grantee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgGrant";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.Authz.MessageGrant(Granter.Address, Grantee.Address);
        }
    }
}