using CosmosNetwork.Modules.Authz.Authorizations;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageExecute(
        CosmosAddress Grantee,
        IAuthorization[] Authorizations) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.msgauth.v1beta1.MsgExecAuthorized";

        protected internal override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageExecute(Grantee.Address)
            {
                Authorizations = Authorizations.Select(msg => msg.ToSerialization()).ToArray()
            };
        }
    }
}