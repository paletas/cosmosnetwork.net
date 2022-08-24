namespace CosmosNetwork.Tendermint.Types
{
    public record SignedHeader(
        Header Header,
        Commit Commit)
    {
        public Serialization.SignedHeader ToSerialization()
        {
            return new Serialization.SignedHeader(
                this.Header.ToSerialization(),
                this.Commit.ToSerialization());
        }
    }
}
