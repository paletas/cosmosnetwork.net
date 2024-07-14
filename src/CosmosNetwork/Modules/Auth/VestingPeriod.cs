namespace CosmosNetwork.Modules.Auth
{
    public record VestingPeriod(TimeSpan Duration, Coin[] Amounts)
    {
        internal Serialization.VestingPeriod ToSerialization()
        {
            return new Serialization.VestingPeriod(
                (long)this.Duration.TotalSeconds,
                this.Amounts.Select(c => c.ToSerialization()).ToArray());
        }
    }
}
