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

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageInstantiateContract(
                Sender.Address,
                Admin?.Address,
                CodeId,
                Label,
                JsonDocument.Parse(InitMessage),
                InitCoins.Select(coin => new DenomAmount(coin.Denom, coin.Amount)).ToArray());
        }
    }
}