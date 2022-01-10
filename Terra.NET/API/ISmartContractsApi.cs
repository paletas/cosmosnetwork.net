namespace Terra.NET.API
{
    public interface ISmartContractsApi
    {
        IAsyncEnumerable<(SmartContract, SmartContractCode)> ListSmartContracts(long? startFromOffset = null, CancellationToken cancellationToken = default);

        Task<SmartContract?> GetSmartContract(TerraAddress contractAddress, CancellationToken cancellationToken = default);

        Task<SmartContractCode?> GetSmartContractCode(string codeId, CancellationToken cancellationToken = default);

        Task<TResponse?> Query<TRequest, TResponse>(TerraAddress contractAddress, TRequest request, CancellationToken cancellationToken = default)
            where TResponse : class;
    }
}
