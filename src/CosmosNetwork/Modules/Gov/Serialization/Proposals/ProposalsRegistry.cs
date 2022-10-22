namespace CosmosNetwork.Modules.Gov.Serialization.Proposals
{
    public class ProposalsRegistry
    {
        private readonly IDictionary<string, Type> _proposalsRegistry = new Dictionary<string, Type>();

        public void Register<T>(string typeName)
            where T : IProposal
        {
            if (_proposalsRegistry.ContainsKey(typeName) == false)
            {
                _proposalsRegistry.Add(typeName, typeof(T));
            }
        }

        internal Type GetProposalByTypeName(string typeName)
        {
            return _proposalsRegistry[typeName];
        }
    }
}
