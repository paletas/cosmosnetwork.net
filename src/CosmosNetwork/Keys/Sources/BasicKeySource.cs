namespace CosmosNetwork.Keys.Sources
{
    internal abstract class BasicKeySource : IKeySource
    {
        public IPrivateKey PrivateKey { get; private set; } = null!;

        public IPublicKey PublicKey { get; private set; } = null!;

        protected void SetKeys(IPrivateKey privateKey, IPublicKey publicKey)
        {
            this.PrivateKey = privateKey;
            this.PublicKey = publicKey;
        }
    }
}
