using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Terra.NET.API.Serialization.Json.Responses;

namespace Terra.NET.API.Impl
{
    internal class MemPoolApi : BaseApiSection, IMemPoolApi
    {
        public MemPoolApi(TerraApiOptions options, HttpClient httpClient, ILogger<MemPoolApi> logger) : base(options, httpClient, logger)
        {
        }

        public async IAsyncEnumerable<MemPoolTransaction> GetPendingTransactions([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var transactions = await Get<MemPoolTransactionsResponse>("/v1/mempool", cancellationToken).ConfigureAwait(false);
            if (transactions == null) throw new InvalidOperationException();

            foreach (var transaction in transactions.Transactions)
            {
                yield return transaction.ToModel();
            }
        }
    }
}
