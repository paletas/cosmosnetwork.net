namespace CosmosNetwork.Tendermint.Types
{
    public record BlockId(byte[] Hash, PartSetHeader PartSetHeader)
    {
        public Serialization.BlockId ToSerialization()
        {
            return new Serialization.BlockId(this.Hash, this.PartSetHeader.ToSerialization());
        }
    }
}
