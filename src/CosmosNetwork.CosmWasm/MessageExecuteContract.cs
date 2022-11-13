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

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.MessageExecuteContract(
                this.Coins.Select(coin => coin.ToSerialization()).ToArray(),
                this.Sender.Address,
                this.Contract.Address,
                JsonDocument.Parse(this.ExecuteMessage));
        }
    }
}