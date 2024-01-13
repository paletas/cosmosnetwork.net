namespace CosmosNetwork.Modules.Bank
{
    public record BankBalance(CosmosAddress Address, Coin[] Coins)
    {
        internal Serialization.BankBalance ToSerialization()
        {
            return new Serialization.BankBalance
            {
                Address = this.Address,
                Coins = this.Coins.Select(c => c.ToSerialization()).ToArray()
            };
        }
    }
}
