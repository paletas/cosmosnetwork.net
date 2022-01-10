using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Responses
{
    internal record GetSmartContractCode([property: JsonPropertyName("code_info")] SmartContractCode Code);

    internal record GetSmartContract([property: JsonPropertyName("contract_info")] SmartContract SmartContract);

    internal record ListSmartContracts(IEnumerable<SmartContract> Contracts, int Limit, long? Next);

    internal record QueryResult<T>([property: JsonPropertyName("query_result")] T Result);
}
