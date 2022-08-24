using CosmosNetwork.Modules;

namespace CosmosNetwork.CosmWasm
{
    public class WasmModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageClearContractAdmin>();
            messageRegistry.RegisterMessage<MessageExecuteContract>();
            messageRegistry.RegisterMessage<MessageInstantiateContract>();
            messageRegistry.RegisterMessage<MessageMigrateContract>();
            messageRegistry.RegisterMessage<MessageMigrateContractCode>();
            messageRegistry.RegisterMessage<MessageStoreContractCode>();
            messageRegistry.RegisterMessage<MessageUpdateContractAdmin>();
        }
    }
}
