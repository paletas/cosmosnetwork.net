namespace CosmosNetwork.Tendermint.Types.Version
{
    public record Consensus(ulong Block, ulong App)
    {
        public Serialization.Version.Consensus ToSerialization()
        {
            return new Serialization.Version.Consensus(this.Block, this.App);
        }
    }
}
