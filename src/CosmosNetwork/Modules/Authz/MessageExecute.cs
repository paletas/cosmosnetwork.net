using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    public record MessageExecute(
        string MessageType,
        CosmosAddress Grantee,
        Message[] Messages) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.authz.v1beta1.MsgExec";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageExecute(this.Grantee.Address)
            {
                Messages = this.Messages.Select(msg => msg.ToSerialization()).ToArray()
            };
        }
    }
}