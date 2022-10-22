using System.Text.Json;

namespace Terra.NET.Messages.Wasm
{
    public record MessageMigrateContract(TerraAddress Admin, TerraAddress Contract, ulong NewCodeId, string MigrateMessage)
        : Message(MessageTypeEnum.WasmMigrateContract)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageMigrateContract(
                this.Admin.Address,
                this.Contract.Address,
                this.NewCodeId,
                JsonDocument.Parse(this.MigrateMessage));
        }
    }
}