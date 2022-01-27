using System.Runtime.Serialization;

namespace Terra.NET.Exceptions
{
    internal class TerraApiException : Exception
    {
        public TerraApiException()
        {
        }

        public TerraApiException(string? message) : base(message)
        {
        }

        public TerraApiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TerraApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
