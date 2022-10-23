using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageExecute(
        CosmosAddress Grantee,
        Message[] Messages) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgExecAuthorized";

        protected internal override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageExecute(this.Grantee.Address)
            {
                Messages = this.Messages.Select(msg => msg.ToSerialization()).ToArray()
            };
        }
    }
}