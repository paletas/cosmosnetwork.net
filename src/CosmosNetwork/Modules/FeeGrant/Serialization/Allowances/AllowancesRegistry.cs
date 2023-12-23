namespace CosmosNetwork.Modules.FeeGrant.Serialization.Allowances
{
    public class AllowancesRegistry
    {
        private readonly IDictionary<string, Type> _allowancesRegistry = new Dictionary<string, Type>();

        public void Register<T>(string typeName)
            where T : IAllowance
        {
            if (this._allowancesRegistry.ContainsKey(typeName) == false)
            {
                this._allowancesRegistry.Add(typeName, typeof(T));
            }
        }

        public Type GetProposalByTypeName(string typeName)
        {
            return this._allowancesRegistry[typeName];
        }
    }
}
