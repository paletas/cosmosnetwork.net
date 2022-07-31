using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm.Classic
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageExecuteContract(DenomAmount[] Coins, [property: JsonPropertyName("sender")] string SenderAddress, [property: JsonPropertyName("contract")] string ContractAddress, [property: JsonPropertyName("execute_msg")] JsonDocument ExecuteMessage)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgExecuteContract";
        public const string COSMOS_DESCRIPTOR = "/terra.wasm.v1beta1.MsgExecuteContract";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Wasm.MessageExecuteContract(this.Coins.Select(c => c.ToModel()).ToArray(), this.SenderAddress, this.ContractAddress, ToJsonString(this.ExecuteMessage));
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            string executeMessageJson = JsonSerializer.Serialize(this.ExecuteMessage, serializerOptions);

            var execute = new MsgExecuteContract
            {
                Sender = this.SenderAddress,
                Contract = this.ContractAddress,
                ExecuteMsg = ByteString.CopyFrom(executeMessageJson, Encoding.UTF8)
            };
            execute.Coins.AddRange(this.Coins.Select(c => c.ToProto()));
            return execute;
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
