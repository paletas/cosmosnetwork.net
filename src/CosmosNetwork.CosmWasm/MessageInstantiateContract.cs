using CosmosNetwork.Serialization;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageInstantiateContract(
        CosmosAddress Sender,
        CosmosAddress? Admin,
        ulong CodeId,
        string Label,
        string InitMessage,
        Coin[] InitCoins) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgInstantiateContract";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageInstantiateContract(
                this.Sender.Address,
                this.Admin?.Address,
                this.CodeId,
                this.Label,
                JsonDocument.Parse(this.InitMessage),
                this.InitCoins.Select(coin => coin.ToSerialization()).ToArray());
        }
    }
}