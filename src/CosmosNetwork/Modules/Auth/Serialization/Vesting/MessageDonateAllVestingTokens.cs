using CosmosNetwork.Serialization;

namespace CosmosNetwork.Modules.Auth.Serialization.Vesting
{
    public record MessageDonateAllVestingTokens(string FromAddress) : SerializerMessage(Auth.Vesting.MessageDonateAllVestingTokens.COSMOS_DESCRIPTOR)
    {
        public override Message ToModel()
        {
            return new Auth.Vesting.MessageDonateAllVestingTokens(this.TypeUrl, this.FromAddress);
        }
    }
}
