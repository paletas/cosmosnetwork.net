using CosmosNetwork.Serialization.Json.Responses;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web;

namespace CosmosNetwork.API.Impl
{
    internal class SmartContractsApi : BaseApiSection, ISmartContractsApi
    {
        public SmartContractsApi(CosmosApiOptions options, HttpClient httpClient, ILogger<SmartContractsApi> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<SmartContractCode?> GetSmartContractCode(string codeId, CancellationToken cancellationToken = default)
        {
            string endpoint = $"/terra/wasm/v1beta1/codes/{codeId}";

            GetSmartContractCode? smartContractCode = await Get<GetSmartContractCode>(endpoint, cancellationToken).ConfigureAwait(false);
            return smartContractCode == null ? null : smartContractCode.Code.ToModel();
        }

        public async Task<SmartContract?> GetSmartContract(CosmosAddress contractAddress, CancellationToken cancellationToken = default)
        {
            string endpoint = $"/terra/wasm/v1beta1/contracts/{contractAddress}";

            GetSmartContract? smartContract = await Get<GetSmartContract>(endpoint, cancellationToken).ConfigureAwait(false);
            return smartContract == null ? null : smartContract.SmartContract.ToModel();
        }

        public async IAsyncEnumerable<(SmartContract, SmartContractCode)> ListSmartContracts(long? startFromOffset = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            string endpoint = $"/v1/wasm/contracts";

            string firstSearchEndpoint = endpoint;
            if (startFromOffset != null)
            {
                firstSearchEndpoint = $"{endpoint}?offset={startFromOffset}";
            }

            ListSmartContracts? smartContracts = await Get<ListSmartContracts>(firstSearchEndpoint, cancellationToken).ConfigureAwait(false);
            while (smartContracts != null && smartContracts.Contracts.Any())
            {
                foreach (Serialization.Json.SmartContract smartContractDetails in smartContracts.Contracts)
                {
                    SmartContract smartContract = smartContractDetails.ToModel();
                    SmartContractCode code = smartContractDetails.Code.ToModel();

                    yield return (smartContract, code);
                }

                if (smartContracts.Next == null || smartContracts.Next > smartContracts.Contracts.Max(contract => contract.Id))
                {
                    break;
                }

                if (Options.ThrottlingEnumeratorsInMilliseconds.HasValue)
                {
                    await Task.Delay(Options.ThrottlingEnumeratorsInMilliseconds.Value, cancellationToken).ConfigureAwait(false);
                }

                smartContracts = await Get<ListSmartContracts>($"{endpoint}?offset={smartContracts.Next}", cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<TResponse?> Query<TRequest, TResponse>(CosmosAddress contractAddress, TRequest request, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            string requestMessage = JsonSerializer.Serialize(request, JsonSerializerOptions);
            string encodedMessage = EncodeToUrl(EncodeTo64(requestMessage));

            string endpoint = $"/terra/wasm/v1beta1/contracts/{contractAddress.Address}/store?query_msg={encodedMessage}";

            QueryResult<TResponse>? queryResult = await Get<QueryResult<TResponse>>(endpoint, cancellationToken).ConfigureAwait(false);
            return queryResult?.Result;
        }

        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(toEncode);
            string returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        private static string EncodeToUrl(string toEncode)
        {
            return HttpUtility.HtmlEncode(toEncode);
        }
    }
}
