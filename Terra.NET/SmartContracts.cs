namespace Terra.NET
{
    public record SmartContract(TerraAddress ContractAddress, string Hash, string Name, string Description, string Memo, DateTime Timestamp, TerraAddress OwnerAddress, string CodeId, string InitializeMessage);

    public record SmartContractCode(string CodeId, string Hash, string Name, string Description, string Memo, DateTime Timestamp, TerraAddress SenderAddress, Uri? RepositoryUrl = null);
}
