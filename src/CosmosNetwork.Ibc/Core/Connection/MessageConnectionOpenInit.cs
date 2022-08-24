using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    public record MessageConnectionOpenInit(
        string ClientId,
        Counterparty Counterparty,
        Version Version,
        ulong DelayPeriod,
        string Signer) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Core.Connection.MessageConnectionOpenInit(
                this.ClientId,
                this.Counterparty.ToSerialization(),
                this.Version.ToSerialization(),
                this.DelayPeriod,
                this.Signer);
        }
    }
}
