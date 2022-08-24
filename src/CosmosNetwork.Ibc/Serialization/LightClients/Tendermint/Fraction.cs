namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    internal record Fraction(ulong Numerator, ulong Denominator)
    {
        public Ibc.LightClients.Tendermint.Fraction ToModel()
        {
            return new Ibc.LightClients.Tendermint.Fraction(this.Numerator, this.Denominator);
        }
    }
}
