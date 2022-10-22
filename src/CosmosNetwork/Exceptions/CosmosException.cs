using System.Runtime.Serialization;

namespace CosmosNetwork.Exceptions
{
    internal class CosmosException : Exception
    {
        public CosmosException()
        {
        }

        public CosmosException(string? message) : base(message)
        {
        }

        public CosmosException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CosmosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
