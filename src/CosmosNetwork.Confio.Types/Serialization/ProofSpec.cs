namespace CosmosNetwork.Confio.Types.Serialization
{
    public record ProofSpec(
        LeafOp LeafSpec,
        InnerSpec InnerSpec,
        int MaxDepth,
        int MinDepth)
    {
        public Types.ProofSpec ToModel()
        {
            return new Types.ProofSpec(
                this.LeafSpec.ToModel(),
                this.InnerSpec.ToModel(),
                this.MaxDepth,
                this.MinDepth);
        }
    }
}