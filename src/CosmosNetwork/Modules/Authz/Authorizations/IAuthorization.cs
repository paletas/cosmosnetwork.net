namespace CosmosNetwork.Modules.Authz.Authorizations
{
    public interface IAuthorization
    {
        string MessageType { get; }

        Serialization.Authorizations.IAuthorization ToSerialization();
    }
}
