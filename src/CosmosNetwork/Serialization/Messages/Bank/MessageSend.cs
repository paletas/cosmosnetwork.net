using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Messages.Bank
{
    internal record MessageSend(
        string FromAddress,
        string ToAddress,
        [property: JsonPropertyName("amount")] DenomAmount[] Coins) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new CosmosNetwork.Messages.Bank.MessageSend(
                FromAddress,
                ToAddress,
                Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}
