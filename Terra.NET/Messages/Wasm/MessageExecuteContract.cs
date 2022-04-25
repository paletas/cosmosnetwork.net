using System.Text.Json;

namespace Terra.NET.Messages.Wasm
{
    public record MessageExecuteContract(Coin[] Coins, TerraAddress Sender, TerraAddress Contract, string ExecuteMessage) : Message(MessageTypeEnum.WasmExecuteContract)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageExecuteContract(
                this.Coins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray(),
                this.Sender.Address,
                this.Contract.Address,
                JsonDocument.Parse(this.ExecuteMessage)
            );
        }
    }
}