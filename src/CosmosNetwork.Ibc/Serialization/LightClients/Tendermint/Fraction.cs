using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.LightClients.Tendermint
{
    [ProtoContract]
    internal record Fraction(
        [property: ProtoMember(1, Name = "numerator")] ulong Numerator, 
        [property: ProtoMember(2, Name = "denominator")] ulong Denominator)
    {
        public Ibc.LightClients.Tendermint.Fraction ToModel()
        {
            return new Ibc.LightClients.Tendermint.Fraction(this.Numerator, this.Denominator);
        }
    }
}
