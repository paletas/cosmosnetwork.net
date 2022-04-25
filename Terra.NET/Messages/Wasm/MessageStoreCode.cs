namespace Terra.NET.Messages.Wasm
{
    public record MessageStoreCode(TerraAddress Sender, string WasmByteCode)
        : Message(MessageTypeEnum.WasmStoreCode)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageStoreCode(this.Sender.Address, this.WasmByteCode);
        }
    }
}