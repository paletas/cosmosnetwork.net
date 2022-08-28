using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CosmosNetwork.CosmWasm.Serialization
{
    [ProtoContract]
    internal record MessageExecuteContract(
        [property: ProtoMember(5, Name = "funds"), JsonPropertyName("funds")] DenomAmount[] Coins,
        [property: ProtoMember(1, Name = "sender"), JsonPropertyName("sender")] string SenderAddress,
        [property: ProtoMember(2, Name = "contract"), JsonPropertyName("contract")] string ContractAddress,
        [property: ProtoMember(3, Name = "msg"), JsonPropertyName("msg")] JsonDocument ExecuteMessage) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new CosmWasm.MessageExecuteContract(
                Coins.Select(c => c.ToModel()).ToArray(),
                SenderAddress,
                ContractAddress,
                ToJsonString(ExecuteMessage));
        }

        private static string ToJsonString(JsonDocument jdoc)
        {
            using MemoryStream stream = new();
            Utf8JsonWriter writer = new(stream, new JsonWriterOptions { Indented = true });
            jdoc.WriteTo(writer);
            writer.Flush();
            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}
