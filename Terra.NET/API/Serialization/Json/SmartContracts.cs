using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json
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
        public Terra.NET.SmartContract ToModel()
        {
            return new Terra.NET.SmartContract(
                this.ContractAddress,
                this.Hash,
                this.Information.Name,
                this.Information.Description,
                this.Information.Memo,
                this.Timestamp,
                this.OwnerAddress,
                this.CodeId,
                this.InitializeMessage
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
        public Terra.NET.SmartContractCode ToModel()
        {
            Uri? repositoryUrl = null;
            if (this.Information.RepositoryUrl != null)
                repositoryUrl = new Uri(this.Information.RepositoryUrl);

            return new Terra.NET.SmartContractCode(
                this.CodeId,
                this.Hash,
                this.Information.Name,
                this.Information.Description,
                this.Information.Memo,
                this.Timestamp,
                this.Sender,
                repositoryUrl
            );
        }
    };

    internal record SmartContractInformation(string Name, string Description, string Memo, [property: JsonPropertyName("repo_url")] string RepositoryUrl);

    internal record SmartContractQueryResult<T>([property: JsonPropertyName("query_result")] T Message);
}
