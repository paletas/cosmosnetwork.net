using System.Text.Json.Serialization;

namespace CosmosNetwork.Serialization.Json
{
    internal record SmartContract(
        [property: JsonPropertyName("id")] long Id,
        [property: JsonPropertyName("contract_address")] string ContractAddress,
        [property: JsonPropertyName("txhash")] string Hash,
        DateTime Timestamp,
        [property: JsonPropertyName("owner")] string OwnerAddress,
        [property: JsonPropertyName("code_id")] string CodeId,
        [property: JsonPropertyName("init_msg")] string InitializeMessage,
        [property: JsonPropertyName("info")] SmartContractInformation Information,
        SmartContractCode Code
    )
    {
        public CosmosNetwork.SmartContract ToModel()
        {
            return new CosmosNetwork.SmartContract(
                ContractAddress,
                Hash,
                Information.Name,
                Information.Description,
                Information.Memo,
                Timestamp,
                OwnerAddress,
                CodeId,
                InitializeMessage
            );
        }
    };

    internal record SmartContractCode(
        [property: JsonPropertyName("code_id")] string CodeId,
        [property: JsonPropertyName("txhash")] string Hash,
        DateTime Timestamp,
        string Sender,
        [property: JsonPropertyName("info")] SmartContractInformation Information
    )
    {
        public CosmosNetwork.SmartContractCode ToModel()
        {
            Uri? repositoryUrl = null;
            if (Information.RepositoryUrl != null)
                repositoryUrl = new Uri(Information.RepositoryUrl);

            return new CosmosNetwork.SmartContractCode(
                CodeId,
                Hash,
                Information.Name,
                Information.Description,
                Information.Memo,
                Timestamp,
                Sender,
                repositoryUrl
            );
        }
    };

    internal record SmartContractInformation(string Name, string Description, string Memo, [property: JsonPropertyName("repo_url")] string RepositoryUrl);

    internal record SmartContractQueryResult<T>([property: JsonPropertyName("query_result")] T Message);
}
