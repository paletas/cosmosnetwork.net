namespace CosmosNetwork.Exceptions
{
    internal class InvalidResponseException : CosmosException
    {
        public InvalidResponseException(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; }
    }
}
