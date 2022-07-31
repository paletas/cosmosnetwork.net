namespace CosmosNetwork.API
{
    public interface ISmartContractsApi
    {
        IAsyncEnumerable<(SmartContract, SmartContractCode)> ListSmartContracts(long? startFromOffset = null, CancellationToken cancellationToken = default);

        Task<SmartContract?> GetSmartContract(CosmosAddress contractAddress, CancellationToken cancellationToken = default);

        Task<SmartContractCode?> GetSmartContractCode(string codeId, CancellationToken cancellationToken = default);

        Task<TResponse?> Query<TRequest, TResponse>(CosmosAddress contractAddress, TRequest request, CancellationToken cancellationToken = default)
            where TResponse : class;
    }
}
