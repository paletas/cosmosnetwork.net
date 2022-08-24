using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Applications.Transfer
{
    public record MessageTransfer(
        string SourcePort,
        string SourceChannel,
        Coin Token,
        CosmosAddress Sender,
        CosmosAddress Receiver,
        Height TimeoutHeight,
        ulong TimeoutTimestamp) : Message
    {
        protected override SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Transfer.MessageTransfer(
                this.SourcePort,
                this.SourceChannel,
                this.Token.ToSerialization(),
                this.Sender.Address,
                this.Receiver.Address,
                this.TimeoutHeight.ToSerialization(),
                this.TimeoutTimestamp);
        }
    }
}
