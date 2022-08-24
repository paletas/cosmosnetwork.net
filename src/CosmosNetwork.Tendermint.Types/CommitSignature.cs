namespace CosmosNetwork.Tendermint.Types
{
    public record CommitSignature(
        BlockIdFlagEnum BlockIdFlag,
        byte[] ValidatorAddress,
        DateTime Timestamp,
        byte[] Signature)
    {
        public Serialization.CommitSig ToSerialization()
        {
            return new Serialization.CommitSig(
                (Serialization.BlockIdFlagEnum)this.BlockIdFlag,
                this.ValidatorAddress,
                this.Timestamp,
                this.Signature);
        }
    }
}
