namespace CosmosNetwork.Serialization.Json
{
    internal record AccountInformation(string AccountNumber, ulong? Sequence);

    internal record AccountBalances(DenomAmount[] Balances);
}
