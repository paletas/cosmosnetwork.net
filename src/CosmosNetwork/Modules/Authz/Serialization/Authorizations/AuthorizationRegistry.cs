namespace CosmosNetwork.Modules.Authz.Serialization.Authorizations
{
    public class AuthorizationRegistry
    {
        private readonly IDictionary<string, Type> _proposalRegistry = new Dictionary<string, Type>();

        public static AuthorizationRegistry Instance { get; private set; } = null!;

        public AuthorizationRegistry()
        {
            Instance = this;
        }

        public void Register<T>(string typeName)
            where T : IAuthorization
        {
            if (this._proposalRegistry.ContainsKey(typeName) == false)
            {
                this._proposalRegistry.Add(typeName, typeof(T));
            }
        }

        public Type GetProposalByTypeName(string typeName)
        {
            return this._proposalRegistry.ContainsKey(typeName) ? this._proposalRegistry[typeName] : typeof(GenericAuthorization);
        }
    }
}
