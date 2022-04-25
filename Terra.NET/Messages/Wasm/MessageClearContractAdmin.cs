namespace Terra.NET.Messages.Wasm
{
    public record MessageClearContractAdmin(TerraAddress Admin, TerraAddress Contract)
        : Message(MessageTypeEnum.WasmClearContractAdmin)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageClearContractAdmin(
                this.Admin.Address,
                this.Contract.Address
            );
        }
    }
}