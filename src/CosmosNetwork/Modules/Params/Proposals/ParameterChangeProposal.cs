using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Params.Proposals
{
    public record ParameterChangeProposal(
        string Title,
        string Description,
        ParamChange[] Changes) : IProposal
    {
        public Gov.Serialization.Proposals.IProposal ToSerialization()
        {
            return new Serialization.Proposals.ParameterChangeProposal(
                this.Title,
                this.Description,
                this.Changes.Select(c => c.ToSerialization()).ToArray());
        }
    }

    public record ParamChange(string Subspace, string Key, string Value)
    {
        internal Serialization.Proposals.ParamChange ToSerialization()
        {
            return new Serialization.Proposals.ParamChange(this.Subspace, this.Key, this.Value);
        }
    }
}
