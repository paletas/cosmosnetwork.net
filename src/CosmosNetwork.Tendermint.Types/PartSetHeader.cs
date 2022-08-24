namespace CosmosNetwork.Tendermint.Types
{
    public record PartSetHeader(uint Total, byte[] Hash)
    {
        public Serialization.PartSetHeader ToSerialization()
        {
            return new Serialization.PartSetHeader(this.Total, this.Hash);
        }
    }
}
