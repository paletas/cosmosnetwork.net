namespace CosmosNetwork.CosmWasm
{
    public record ContractCodeHistoryEntry(
        ContractCodeHistoryOperationTypes Operation,
        ulong CodeId,
        EntryUpdatedAt UpdatedAt,
        string RawMessage)
    {

    }

    public record EntryUpdatedAt(ulong BlockHeight, ulong TransactionIndex)
    {

    }

    public enum ContractCodeHistoryOperationTypes
    {
        Unspecified = Serialization.ContractCodeHistoryOperationTypes.CONTRACT_CODE_HISTORY_OPERATION_TYPE_UNSPECIFIED,
        Initialization = Serialization.ContractCodeHistoryOperationTypes.CONTRACT_CODE_HISTORY_OPERATION_TYPE_INIT,
        Migration = Serialization.ContractCodeHistoryOperationTypes.CONTRACT_CODE_HISTORY_OPERATION_TYPE_MIGRATE,
        Genesis = Serialization.ContractCodeHistoryOperationTypes.CONTRACT_CODE_HISTORY_OPERATION_TYPE_GENESIS,
    }
}
