using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    public record MessageGrant(
        string MessageType,
        CosmosAddress Granter,
        CosmosAddress Grantee,
        Grant Grant) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.authz.v1beta1.MsgGrant";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageGrant(this.Granter.Address, this.Grantee.Address, this.Grant.ToSerialization());
        }
    }
}