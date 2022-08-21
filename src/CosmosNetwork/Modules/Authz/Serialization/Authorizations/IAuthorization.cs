using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    public interface IAuthorization : IHasAny
    {
        new string TypeUrl { get; }

        Authz.Authorizations.IAuthorization ToModel();
    }
}
