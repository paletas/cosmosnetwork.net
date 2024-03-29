using CosmosNetwork.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace CosmosNetwork.CosmWasm.API.Impl
{
    internal class CosmWasmApi(
        [ServiceKey] string servicesKey,
        IServiceProvider serviceProvider,
        IHttpClientFactory httpClientFactory,
        ILogger<CosmWasmApi> logger) : CosmosApiModule(servicesKey, serviceProvider, httpClientFactory, logger), ICosmWasmApi
    {
        public async IAsyncEnumerable<ContractCodeHistoryEntry> GetContractHistory(CosmosAddress contract, bool orderReversed = false, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            Serialization.Responses.ContractHistoryResponse? contractHistoryResponse 
                = await GetContractHistory(contract, nextKey: null, orderReversed, cancellationToken);

            if (contractHistoryResponse is null)
            {
                yield break;
            }

            do
            {
                foreach (Serialization.ContractCodeHistoryEntry entry in contractHistoryResponse.Entries)
                {
                    yield return entry.ToModel();
                }

                if (contractHistoryResponse.Pagination.NextKey is not null)
                {
                    contractHistoryResponse = await GetContractHistory(contract, contractHistoryResponse.Pagination.NextKey, orderReversed, cancellationToken); ;

                    if (contractHistoryResponse is null)
                    {
                        yield break;
                    }
                }
            }
            while (contractHistoryResponse.Pagination.NextKey is not null);

            Task<Serialization.Responses.ContractHistoryResponse?> GetContractHistory(CosmosAddress contract, string? nextKey, bool orderReversed, CancellationToken cancellationToken = default)
            {
                string uri = $"cosmwasm/wasm/v1/contract/{contract.Address}/history?";
                bool firstQueryArgument = false;
                if (nextKey is not null)
                {
                    if (firstQueryArgument)
                    {
                        uri += "&";
                        firstQueryArgument = false;
                    }
                    uri += $"pagination.key={nextKey}";
                }

                if (orderReversed)
                {
                    if (firstQueryArgument)
                    {
                        uri += "&";
                        firstQueryArgument = false;
                    }
                    uri += "pagination.order_by=desc";
                }

                return this.Get<Serialization.Responses.ContractHistoryResponse>(uri, cancellationToken: cancellationToken);
            }
        }
    }
}
