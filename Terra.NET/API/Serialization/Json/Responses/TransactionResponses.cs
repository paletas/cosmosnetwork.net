using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Responses
{
    internal record ListTransactionsResponse(long? Next, int Limit, [property: JsonPropertyName("txs")] BlockTransaction[] Transactions);

    internal record TransactionSimulationResponse(TransactionGasUsage GasInfo, TransactionSimulationResult Result);

    internal record TransactionBroadcastResponse([property: JsonPropertyName("tx_response")] TransactionResponse Result);

    internal record ComputeTaxResponse(DenomAmount[] TaxAmount);

    internal record MemPoolTransactionsResponse([property: JsonPropertyName("txs")] MemPoolTransaction[] Transactions);
}
