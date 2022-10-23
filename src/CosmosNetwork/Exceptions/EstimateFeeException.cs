namespace CosmosNetwork.Exceptions
{
    internal class EstimateFeeException : CosmosException
    {
        public EstimateFeeException(uint errorCode, string? message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public uint ErrorCode { get; }
    }
}
