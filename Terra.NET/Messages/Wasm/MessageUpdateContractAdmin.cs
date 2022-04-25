namespace Terra.NET.Messages.Wasm
{
    public record MessageUpdateContractAdmin(TerraAddress Admin, TerraAddress NewAdmin, TerraAddress Contract)
        : Message(MessageTypeEnum.WasmUpdateContractAdmin)
    {
        internal override API.Serialization.Json.Message ToJson()
        {
            return new API.Serialization.Json.Messages.Wasm.MessageUpdateContractAdmin(
                this.Admin.Address,
                this.NewAdmin.Address,
                this.Contract.Address
            );
        }
    }
}