using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    public interface IAuthorization : IHasAny
    {
        Authz.Authorizations.IAuthorization ToModel();
    }
}
