namespace Terra.NET.API.Serialization.Json
{
    internal record AccountInformation(string AccountNumber, ulong? Sequence);

    internal record AccountBalances(DenomAmount[] Balances);
}
