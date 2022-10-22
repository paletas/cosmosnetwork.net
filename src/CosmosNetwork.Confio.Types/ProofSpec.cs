namespace CosmosNetwork.Confio.Types
{
    public record ProofSpec(
        LeafOp LeafSpec,
        InnerSpec InnerSpec,
        int MaxDepth,
        int MinDepth)
    {
        public Serialization.ProofSpec ToSerialization()
        {
            return new Serialization.ProofSpec(
                LeafSpec.ToSerialization(),
                InnerSpec.ToSerialization(),
                MaxDepth,
                MinDepth);
        }
    }
}