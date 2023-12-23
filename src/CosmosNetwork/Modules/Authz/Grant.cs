using CosmosNetwork.Modules.Authz.Authorizations;

namespace CosmosNetwork.Modules.Authz
{
    public class Grant
    {
        public IAuthorization Authorization { get; set; } = null!;

        public DateTime Expiration { get; set; }

        internal Modules.Authz.Serialization.Grant ToSerialization()
        {
            IAuthorization? authorizationSerializable = this.Authorization;
            return authorizationSerializable is null
                ? throw new InvalidProgramException()
                : new Serialization.Grant
                {
                    Authorization = authorizationSerializable.ToSerialization(),
                    Expiration = Expiration
                };
        }
    }
}
