namespace Terra.NET.Exceptions
{
    internal class EstimateFeeException : TerraApiException
    {
        public EstimateFeeException(uint errorCode, string? message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public uint ErrorCode { get; }
    }
}
