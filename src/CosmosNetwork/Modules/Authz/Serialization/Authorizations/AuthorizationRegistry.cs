namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    public class AuthorizationRegistry
    {
        private readonly IDictionary<string, Type> _authorizationRegistry = new Dictionary<string, Type>();

        public static AuthorizationRegistry Instance { get; private set; } = null!;

        public AuthorizationRegistry()
        {
            Instance = this;
        }

        public void Register<T>(string typeName)
            where T : IAuthorization
        {
            if (this._authorizationRegistry.ContainsKey(typeName) == false)
            {
                this._authorizationRegistry.Add(typeName, typeof(T));
            }
        }

        public Type GetAuthorizationByTypeName(string typeName)
        {
            return this._authorizationRegistry.ContainsKey(typeName) ? this._authorizationRegistry[typeName] : typeof(GenericAuthorization);
        }
    }
}
