namespace CosmosNetwork.Modules.Gov.Proposals
{
    public class ProposalRegistry
    {
        private readonly IDictionary<string, Type> _proposalRegistry = new Dictionary<string, Type>();

        public void Register<T>(string typeName)
            where T : IProposal
        {
            _proposalRegistry.Add(typeName, typeof(T));
        }

        internal Type GetProposalByTypeName(string typeName)
        {
            return _proposalRegistry[typeName];
        }
    }
}
