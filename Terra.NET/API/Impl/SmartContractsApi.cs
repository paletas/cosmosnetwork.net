using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web;
using Terra.NET.API.Serialization.Json.Responses;

namespace Terra.NET.API.Impl
{
    internal class SmartContractsApi : BaseApiSection, ISmartContractsApi
    {
        public SmartContractsApi(TerraApiOptions options, HttpClient httpClient, ILogger<SmartContractsApi> logger) : base(options, httpClient, logger)
        {
        }

        public async Task<SmartContractCode?> GetSmartContractCode(string codeId, CancellationToken cancellationToken = default)
        {
            var endpoint = $"/terra/wasm/v1beta1/codes/{codeId}";

            var smartContractCode = await this.Get<GetSmartContractCode>(endpoint, cancellationToken).ConfigureAwait(false);
            if (smartContractCode == null) return null;

            return smartContractCode.Code.ToModel();
        }

        public async Task<SmartContract?> GetSmartContract(TerraAddress contractAddress, CancellationToken cancellationToken = default)
        {
            var endpoint = $"/terra/wasm/v1beta1/contracts/{contractAddress}";

            var smartContract = await this.Get<GetSmartContract>(endpoint, cancellationToken).ConfigureAwait(false);
            if (smartContract == null) return null;

            return smartContract.SmartContract.ToModel();
        }

        public async IAsyncEnumerable<(SmartContract, SmartContractCode)> ListSmartContracts(long? startFromOffset = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var endpoint = $"/v1/wasm/contracts";

            var firstSearchEndpoint = endpoint;
            if (startFromOffset != null) firstSearchEndpoint = $"{endpoint}?offset={startFromOffset}";

            var smartContracts = await this.Get<ListSmartContracts>(firstSearchEndpoint, cancellationToken).ConfigureAwait(false);
            while (smartContracts != null && smartContracts.Contracts.Any())
            {
                foreach (var smartContractDetails in smartContracts.Contracts)
                {
                    var smartContract = smartContractDetails.ToModel();
                    var code = smartContractDetails.Code.ToModel();

                    yield return (smartContract, code);
                }

                if (smartContracts.Next == null || smartContracts.Next > smartContracts.Contracts.Max(contract => contract.Id)) break;

                if (Options.ThrottlingEnumeratorsInMilliseconds.HasValue)
                    await Task.Delay(Options.ThrottlingEnumeratorsInMilliseconds.Value, cancellationToken).ConfigureAwait(false);

                smartContracts = await this.Get<ListSmartContracts>($"{endpoint}?offset={smartContracts.Next}", cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<TResponse?> Query<TRequest, TResponse>(TerraAddress contractAddress, TRequest request, CancellationToken cancellationToken = default)
            where TResponse : class
        {
            var requestMessage = JsonSerializer.Serialize<TRequest>(request, base.JsonSerializerOptions);
            var encodedMessage = EncodeToUrl(EncodeTo64(requestMessage));

            var endpoint = $"/terra/wasm/v1beta1/contracts/{contractAddress.Address}/store?query_msg={encodedMessage}";

            var queryResult = await this.Get<QueryResult<TResponse>>(endpoint, cancellationToken).ConfigureAwait(false);
            return queryResult?.Result;
        }

        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        private static string EncodeToUrl(string toEncode)
        {
            return HttpUtility.HtmlEncode(toEncode);
        }
    }
}
