namespace CosmosNetwork.Modules.FeeGrant
{
    public record MessageRevokeAllowance(string MessageType, CosmosAddress Granter, CosmosAddress Grantee) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.feegrant.v1beta1.MsgRevokeAllowance";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageRevokeAllowance(
                this.Granter.Address,
                this.Grantee.Address);
        }
    }
}