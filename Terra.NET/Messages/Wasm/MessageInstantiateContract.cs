using System.Text.Json;

namespace Terra.NET.Messages.Wasm
{
    public record MessageInstantiateContract(TerraAddress Sender, TerraAddress Admin, ulong CodeId, string InitMessage, Coin[] InitCoins)
        : Message(MessageTypeEnum.WasmInstantiateContract)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageInstantiateContract(
                this.Sender.Address,
                this.Admin.Address,
                this.CodeId,
                JsonDocument.Parse(this.InitMessage),
                this.InitCoins.Select(coin => new API.Serialization.Json.DenomAmount(coin.Denom, coin.Amount)).ToArray()
            );
        }
    }
}