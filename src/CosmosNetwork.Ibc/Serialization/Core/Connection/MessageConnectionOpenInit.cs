using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Core.Connection
{
    [ProtoContract]
    internal record MessageConnectionOpenInit(
        [property: ProtoMember(1, Name = "client_id")] string ClientId,
        [property: ProtoMember(2, Name = "counterparty")] Counterparty Counterparty,
        [property: ProtoMember(3, Name = "version")] Version Version,
        [property: ProtoMember(4, Name = "delay_period")] ulong DelayPeriod,
        [property: ProtoMember(5, Name = "signer")] string Signer) : SerializerMessage(Ibc.Core.Connection.MessageConnectionOpenInit.COSMOS_DESCRIPTOR)
    {
        protected override Message ToModel()
        {
            return new Ibc.Core.Connection.MessageConnectionOpenInit(
                this.ClientId,
                this.Counterparty.ToModel(),
                this.Version.ToModel(),
                this.DelayPeriod,
                this.Signer);
        }
    }
}
