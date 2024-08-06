using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    public record MessageRevoke(
        string MessageType,
        CosmosAddress Granter,
        CosmosAddress Grantee,
        string MessageTypeUrl) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.authz.v1beta1.MsgRevoke";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageRevoke(this.Granter.Address, this.Grantee.Address, this.MessageTypeUrl);
        }
    }
}