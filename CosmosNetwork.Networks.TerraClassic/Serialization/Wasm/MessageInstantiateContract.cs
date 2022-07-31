using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json.Messages.Wasm
{

    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageInstantiateContract([property: JsonPropertyName("sender")] string SenderAddress, [property: JsonPropertyName("admin")] string? AdminAddress, ulong CodeId, string Label, JsonDocument Msg, DenomAmount[] Funds)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "wasm/MsgInstantiateContract";
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgInstantiateContract";

        internal override NET.Message ToModel()
        {
            string initMessageJson = JsonSerializer.Serialize(this.Msg);

            return new NET.Messages.Wasm.MessageInstantiateContract(
                this.SenderAddress,
                string.IsNullOrWhiteSpace(this.AdminAddress) ? null : new TerraAddress(this.AdminAddress),
                this.CodeId,
                this.Label,
                initMessageJson,
                this.Funds.Select(coin => coin.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            string initMessageJson = JsonSerializer.Serialize(this.Msg, serializerOptions);

            var initContract = new MsgInstantiateContract
            {
                Sender = this.SenderAddress,
                Admin = this.AdminAddress,
                CodeId = this.CodeId,
                InitMsg = ByteString.CopyFrom(initMessageJson, Encoding.UTF8)
            };
            initContract.InitCoins.AddRange(this.Funds.Select(c => c.ToProto()));
            return initContract;
        }
    }
}
