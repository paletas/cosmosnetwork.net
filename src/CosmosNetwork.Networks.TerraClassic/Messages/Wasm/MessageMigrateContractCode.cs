namespace Terra.NET.Messages.Wasm
{
    public record MessageMigrateContractCode(TerraAddress Sender, ulong CodeId, string WasmByteCode)
        : Message(MessageTypeEnum.WasmMigrateCode)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageMigrateContractCode(this.Sender.Address, this.CodeId, this.WasmByteCode);
        }
    }
}