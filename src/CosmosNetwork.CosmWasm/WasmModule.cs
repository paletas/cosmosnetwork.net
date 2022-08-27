using CosmosNetwork.Modules;

namespace CosmosNetwork.CosmWasm
{
    public class WasmModule : ICosmosModule
    {
        public void ConfigureModule(CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageClearContractAdmin, Serialization.MessageClearContractAdmin>();
            messageRegistry.RegisterMessage<MessageExecuteContract, Serialization.MessageExecuteContract>();
            messageRegistry.RegisterMessage<MessageInstantiateContract, Serialization.MessageInstantiateContract>();
            messageRegistry.RegisterMessage<MessageMigrateContract, Serialization.MessageMigrateContract>();
            messageRegistry.RegisterMessage<MessageMigrateContractCode, Serialization.MessageMigrateContractCode>();
            messageRegistry.RegisterMessage<MessageStoreContractCode, Serialization.MessageStoreContractCode>();
            messageRegistry.RegisterMessage<MessageUpdateContractAdmin, Serialization.MessageUpdateContractAdmin>();
        }
    }
}
