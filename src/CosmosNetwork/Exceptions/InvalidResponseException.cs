namespace CosmosNetwork.Exceptions
{
    internal class InvalidResponseException : CosmosException
    {
        public InvalidResponseException(string rawResponse)
        {
            this.RawResponse = rawResponse;
        }

        public string RawResponse { get; }
    }
}
