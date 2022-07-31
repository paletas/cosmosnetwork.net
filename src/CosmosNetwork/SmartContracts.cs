namespace CosmosNetwork
{
    public record SmartContract(CosmosAddress ContractAddress, string Hash, string Name, string Description, string Memo, DateTime Timestamp, CosmosAddress OwnerAddress, string CodeId, string InitializeMessage);

    public record SmartContractCode(string CodeId, string Hash, string Name, string Description, string Memo, DateTime Timestamp, CosmosAddress SenderAddress, Uri? RepositoryUrl = null);
}
