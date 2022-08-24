namespace CosmosNetwork.Confio.Serialization
{
    public record ProofSpec(
        LeafOp LeafSpec,
        InnerSpec InnerSpec,
        int MaxDepth,
        int MinDepth)
    {
        public Confio.ProofSpec ToModel()
        {
            return new Confio.ProofSpec(
                this.LeafSpec.ToModel(),
                this.InnerSpec.ToModel(),
                this.MaxDepth,
                this.MinDepth);
        }
    }
}