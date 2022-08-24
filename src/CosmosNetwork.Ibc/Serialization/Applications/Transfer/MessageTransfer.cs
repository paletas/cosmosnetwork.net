using CosmosNetwork.Ibc.Serialization.Core.Client;
using CosmosNetwork.Serialization;
using ProtoBuf;

namespace CosmosNetwork.Ibc.Serialization.Applications.Transfer
{
    [ProtoContract]
    internal record MessageTransfer(
        [property: ProtoMember(1, Name = "source_port")] string SourcePort,
        [property: ProtoMember(2, Name = "source_channel")] string SourceChannel,
        [property: ProtoMember(3, Name = "token")] DenomAmount Token,
        [property: ProtoMember(4, Name = "sender")] string SenderAddress,
        [property: ProtoMember(5, Name = "receiver")] string ReceiverAddress,
        [property: ProtoMember(6, Name = "timeout_height")] Height TimeoutHeight,
        [property: ProtoMember(7, Name = "timeout_timestamp")] ulong TimeoutTimestamp) : SerializerMessage
    {
        protected override Message ToModel()
        {
            return new Ibc.Applications.Transfer.MessageTransfer(
                this.SourcePort,
                this.SourceChannel,
                this.Token.ToModel(),
                this.SenderAddress,
                this.ReceiverAddress,
                this.TimeoutHeight.ToModel(),
                this.TimeoutTimestamp);
        }
    }
}
