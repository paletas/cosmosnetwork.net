namespace CosmosNetwork.Modules.FeeGrant
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageRevokeAllowance(CosmosAddress Granter, CosmosAddress Grantee) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgRevokeAllowance";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageRevokeAllowance(
                Granter.Address,
                Grantee.Address);
        }
    }
}