namespace CosmosNetwork.Modules.Auth
{
    public record VestingPeriod(ulong Length, Coin[] Amounts)
    {
        internal Serialization.Accounts.VestingPeriod ToSerialization()
        {
            return new Serialization.Accounts.VestingPeriod
            {
                Length = Length,
                Amount = Amounts.Select(c => c.ToSerialization()).ToArray(),
            };
        }
    }
}
