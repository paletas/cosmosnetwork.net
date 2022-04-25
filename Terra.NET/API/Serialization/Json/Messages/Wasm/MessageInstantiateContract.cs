using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageInstantiateContract([property: JsonPropertyName("sender")] string SenderAddress, [property: JsonPropertyName("admin")] string AdminAddress, ulong CodeId, JsonDocument InitMsg, DenomAmount[] InitCoins)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgInstantiateContract";
        public const string COSMOS_DESCRIPTOR = "/cosmos.wasm.v1beta1.MsgInstantiateContract";

        internal override NET.Message ToModel()
        {
            string initMessageJson = JsonSerializer.Serialize(this.InitMsg);

            return new NET.Messages.Wasm.MessageInstantiateContract(
                this.SenderAddress,
                this.AdminAddress,
                this.CodeId,
                initMessageJson,
                this.InitCoins.Select(coin => coin.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            string initMessageJson = JsonSerializer.Serialize(this.InitMsg, serializerOptions);

            var initContract = new MsgInstantiateContract
            {
                Sender = this.SenderAddress,
                Admin = this.AdminAddress,
                CodeId = this.CodeId,
                InitMsg = ByteString.CopyFrom(initMessageJson, Encoding.UTF8)
            };
            initContract.InitCoins.AddRange(this.InitCoins.Select(c => c.ToProto()));
            return initContract;
        }
    }
}
