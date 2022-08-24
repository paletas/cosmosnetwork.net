using CosmosNetwork.Tendermint.Types.Crypto;

namespace CosmosNetwork.Tendermint.Types
{
    public record Validator(
        byte[] Address,
        PublicKey PublicKey,
        long VotingPower,
        long ProposerPriority)
    {
        public Serialization.Validator ToSerialization()
        {
            return new Serialization.Validator(
                this.Address,
                this.PublicKey.ToSerialization(),
                this.VotingPower,
                this.ProposerPriority);
        }
    }
}
