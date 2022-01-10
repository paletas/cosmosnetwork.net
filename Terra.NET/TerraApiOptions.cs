namespace Terra.NET
{
    public class TerraApiOptions
    {
        public TerraApiOptions() { }

        public TerraApiOptions(int? throttlingEnumeratorsInSeconds = default, long? startingBlockHeightForTransactionSearch = default)
        {
            if (throttlingEnumeratorsInSeconds != default)
                ThrottlingEnumeratorsInMilliseconds = throttlingEnumeratorsInSeconds;

            if (startingBlockHeightForTransactionSearch.HasValue)
                StartingBlockHeightForTransactionSearch = startingBlockHeightForTransactionSearch.Value;
        }

        public int? ThrottlingEnumeratorsInMilliseconds { get; set; } = 1000;

        public long StartingBlockHeightForTransactionSearch { get; set; } = 1;

        public Coin[]? GasPrices { get; set; } = null;

        public decimal GasAdjustment { get; set; } = 1.75M;

        public string[]? DefaultDenoms { get; set; } = new[] { "uust" };
    }
}
