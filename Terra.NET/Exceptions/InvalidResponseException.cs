namespace Terra.NET.Exceptions
{
    internal class InvalidResponseException : TerraApiException
    {
        public InvalidResponseException(string rawResponse)
        {
            this.RawResponse = rawResponse;
        }

        public string RawResponse { get; }
    }
}
