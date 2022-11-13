using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Core.Connection
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageConnectionOpenInit(
        string ClientId,
        Counterparty Counterparty,
        Version Version,
        ulong DelayPeriod,
        string Signer) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.core.connection.v1.MsgConnectionOpenInit";

        public override SerializerMessage ToSerialization()
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
