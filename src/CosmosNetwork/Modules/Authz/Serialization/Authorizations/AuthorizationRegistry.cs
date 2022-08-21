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
            if (_proposalRegistry.ContainsKey(typeName) == false)
            {
                _proposalRegistry.Add(typeName, typeof(T));
            }
        }

        public Type GetProposalByTypeName(string typeName)
        {
            return _proposalRegistry.ContainsKey(typeName) ? _proposalRegistry[typeName] : typeof(GenericAuthorization);
        }
    }
}
