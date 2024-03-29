namespace CosmosNetwork.CosmWasm.Serialization.Responses
{
    internal record ContractHistoryResponse(IEnumerable<ContractCodeHistoryEntry> Entries, Pagination Pagination)
    {
    }
}
