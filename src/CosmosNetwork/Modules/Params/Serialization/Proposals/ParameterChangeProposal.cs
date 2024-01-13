using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using ProtoBuf;

namespace CosmosNetwork.Modules.Params.Serialization.Proposals
{
    [ProtoContract]
    public record ParameterChangeProposal(
        [property: ProtoMember(1, Name = "title")] string Title,
        [property: ProtoMember(2, Name = "description")] string Description,
        [property: ProtoMember(3, Name = "changes")] ParamChange[] Changes) : IProposal
    {
        public const string ProposalType = "/cosmos.params.v1beta1.ParameterChangeProposal";

        public string TypeUrl => ProposalType;

        public Gov.Proposals.IProposal ToModel()
        {
            return new Params.Proposals.ParameterChangeProposal(
                this.Title,
                this.Description,
                this.Changes.Select(c => c.ToModel()).ToArray());
        }
    }

    [ProtoContract]
    public record ParamChange(
        [property: ProtoMember(1, Name = "subspace")] string Subspace,
        [property: ProtoMember(2, Name = "key")] string Key,
        [property: ProtoMember(3, Name = "value")] string Value)
    {
        public Params.Proposals.ParamChange ToModel()
        {
            return new Params.Proposals.ParamChange(this.Subspace, this.Key, this.Value);
        }
    }
}
