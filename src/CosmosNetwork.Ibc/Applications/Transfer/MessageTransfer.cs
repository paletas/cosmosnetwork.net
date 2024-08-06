using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Serialization;

namespace CosmosNetwork.Ibc.Applications.Transfer
{
    public record MessageTransfer(
        string SourcePort,
        string SourceChannel,
        Coin Token,
        CosmosAddress Sender,
        string Receiver,
        Height TimeoutHeight,
        ulong TimeoutTimestamp) : Message(COSMOS_DESCRIPTOR)
    {
        public const string COSMOS_DESCRIPTOR = "/ibc.applications.transfer.v1.MsgTransfer";

        public override SerializerMessage ToSerialization()
        {
            return new Serialization.Applications.Transfer.MessageTransfer(
                this.SourcePort,
                this.SourceChannel,
                this.Token.ToSerialization(),
                this.Sender.Address,
                this.Receiver,
                this.TimeoutHeight.ToSerialization(),
                this.TimeoutTimestamp);
        }
    }
}
