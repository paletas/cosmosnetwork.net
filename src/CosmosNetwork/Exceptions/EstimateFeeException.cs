namespace CosmosNetwork.Exceptions
{
    internal class EstimateFeeException : CosmosException
    {
        public EstimateFeeException(uint errorCode, string? message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public uint ErrorCode { get; }
    }
}
