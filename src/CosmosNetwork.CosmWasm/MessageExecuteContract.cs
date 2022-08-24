using CosmosNetwork.Serialization;
using System.Text.Json;

namespace CosmosNetwork.CosmWasm
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageExecuteContract(
        Coin[] Coins,
        CosmosAddress Sender,
        CosmosAddress Contract,
        string ExecuteMessage) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmwasm.wasm.v1.MsgExecuteContract";

        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageExecuteContract(
                Coins.Select(coin => new DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                Sender.Address,
                Contract.Address,
                JsonDocument.Parse(ExecuteMessage));
        }
    }
}