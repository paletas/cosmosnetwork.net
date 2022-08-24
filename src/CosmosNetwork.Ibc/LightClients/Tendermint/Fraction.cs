namespace CosmosNetwork.Ibc.LightClients.Tendermint
{
    public record Fraction(ulong Numerator, ulong Denominator)
    {
        internal Serialization.LightClients.Tendermint.Fraction ToSerialization()
        {
            return new Serialization.LightClients.Tendermint.Fraction(this.Numerator, this.Denominator);
        }
    }
}
