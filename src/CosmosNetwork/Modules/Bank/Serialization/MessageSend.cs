using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Bank.Serialization
{
    [ProtoContract]
    internal record MessageSend(
        [property: ProtoMember(1, Name = "from_address")] string FromAddress,
        [property: ProtoMember(2, Name = "to_address")] string ToAddress,
        [property: ProtoMember(3, Name = "amount"), JsonPropertyName("amount")] DenomAmount[] Coins) : SerializerMessage
    {
        protected internal override Message ToModel()
        {
            return new Bank.MessageSend(
                FromAddress,
                ToAddress,
                Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}
