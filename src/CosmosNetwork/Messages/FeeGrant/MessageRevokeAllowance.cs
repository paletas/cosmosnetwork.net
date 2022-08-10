namespace CosmosNetwork.Messages.FeeGrant
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRevokeAllowance(CosmosAddress Granter, CosmosAddress Grantee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgRevokeAllowance";

        protected internal override Serialization.SerializerMessage ToJson()
        {
            return new Serialization.Messages.FeeGrant.MessageRevokeAllowance(
                Granter.Address,
                Grantee.Address);
        }
    }
}