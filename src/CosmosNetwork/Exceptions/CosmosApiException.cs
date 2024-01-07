using System.Net;

namespace CosmosNetwork.Exceptions
{
    internal class CosmosApiException : CosmosException
    {
        public CosmosApiException(HttpStatusCode statusCode, string message)
            : base($"{statusCode} - {message}")
        {
            this.StatusCode = statusCode;
            this.InnerMessage = message;
        }

        public HttpStatusCode StatusCode { get; }

        public string InnerMessage { get; }
    }
}
