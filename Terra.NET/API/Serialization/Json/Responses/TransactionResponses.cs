using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Responses
{
    internal record ListTransactionsResponse([property: JsonPropertyName("txs")] BlockTransaction[] Transactions, Pagination Pagination);

    internal record TransactionSimulationResponse(TransactionGasUsage GasInfo, TransactionSimulationResult Result);

    internal record TransactionBroadcastResponse([property: JsonPropertyName("tx_response")] TransactionResponse Result);

    internal record ComputeTaxResponse(DenomAmount[] TaxAmount);
}
