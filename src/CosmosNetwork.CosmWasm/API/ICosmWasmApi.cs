using System.Runtime.CompilerServices;

namespace CosmosNetwork.CosmWasm.API
{
    public interface ICosmWasmApi
    {
        IAsyncEnumerable<ContractCodeHistoryEntry> GetContractHistory(CosmosAddress contract, bool orderReversed = false, [EnumeratorCancellation] CancellationToken cancellationToken = default);
    }
}
