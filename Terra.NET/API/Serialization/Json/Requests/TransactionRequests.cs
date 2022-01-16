using Cosmos.SDK.Protos.Tx;

namespace Terra.NET.API.Serialization.Json.Requests
{
    internal record TransactionRequest(string TxBytes, string? Mode = null);
}
