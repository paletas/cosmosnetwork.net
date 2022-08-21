namespace CosmosNetwork.Modules.Authz.Authorizations
{
    public class GenericAuthorization : IAuthorization
    {
        public GenericAuthorization(string messageType)
        {
            MessageType = messageType;
        }

        public string MessageType { get; set; }

        public Serialization.Authorizations.IAuthorization ToSerialization()
        {
            return new Authz.Serialization.Authorizations.GenericAuthorization(MessageType);
        }
    }
}
