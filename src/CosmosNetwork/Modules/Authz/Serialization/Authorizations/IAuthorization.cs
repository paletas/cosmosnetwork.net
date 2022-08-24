using CosmosNetwork.Serialization.Proto;

namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    public interface IAuthorization : IHasAny
    {
        new string TypeUrl { get; }

        Authz.Authorizations.IAuthorization ToModel();
    }
}
