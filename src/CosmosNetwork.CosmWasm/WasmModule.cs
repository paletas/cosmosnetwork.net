using CosmosNetwork.CosmWasm.Serialization.Proposals;
using CosmosNetwork.Modules;
using CosmosNetwork.Modules.Gov;

namespace CosmosNetwork.CosmWasm
{
    public class WasmModule : ICosmosModule
    {
        private readonly GovModule _governanceModule;

        public WasmModule(GovModule governanceModule)
        {
            this._governanceModule = governanceModule;
        }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageClearContractAdmin, Serialization.MessageClearContractAdmin>();
            messageRegistry.RegisterMessage<MessageExecuteContract, Serialization.MessageExecuteContract>();
            messageRegistry.RegisterMessage<MessageInstantiateContract, Serialization.MessageInstantiateContract>();
            messageRegistry.RegisterMessage<MessageMigrateContractCode, Serialization.MessageMigrateContractCode>();
            messageRegistry.RegisterMessage<MessageStoreContractCode, Serialization.MessageStoreContractCode>();
            messageRegistry.RegisterMessage<MessageUpdateContractAdmin, Serialization.MessageUpdateContractAdmin>();

            this._governanceModule.ProposalsRegistry.Register<ClearAdminProposal>(ClearAdminProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<ExecuteContractProposal>(ExecuteContractProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<InstantiateContractProposal>(InstantiateContractProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<MigrateContractProposal>(MigrateContractProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<PinCodesProposal>(PinCodesProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<StoreCodeProposal>(StoreCodeProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<SudoContractProposal>(SudoContractProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<UnpinCodesProposal>(UnpinCodesProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<UpdateAdminProposal>(UpdateAdminProposal.ProposalType);
            this._governanceModule.ProposalsRegistry.Register<UpdateInstantiateConfigProposal>(UpdateInstantiateConfigProposal.ProposalType);
        }
    }
}
