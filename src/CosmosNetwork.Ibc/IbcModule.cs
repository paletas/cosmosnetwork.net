using CosmosNetwork.Ibc.Applications.Fees;
using CosmosNetwork.Ibc.Applications.Transfer;
using CosmosNetwork.Ibc.Core.Channel;
using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Ibc.Core.Connection;
using CosmosNetwork.Modules;

namespace CosmosNetwork.Ibc
{
    internal class IbcModule : ICosmosMessageModule
    {
        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessagePayPacketFee, Serialization.Applications.Fees.MessagePayPacketFee>();
            messageRegistry.RegisterMessage<MessagePayPacketFeeAsync, Serialization.Applications.Fees.MessagePayPacketFeeAsync>();
            messageRegistry.RegisterMessage<MessageRegisterCounterpartyAddress, Serialization.Applications.Fees.MessageRegisterCounterpartyAddress>();

            messageRegistry.RegisterMessage<MessageTransfer, Serialization.Applications.Transfer.MessageTransfer>();

            messageRegistry.RegisterMessage<MessageAcknowledgement, Serialization.Core.Channel.MessageAcknowledgement>();
            messageRegistry.RegisterMessage<MessageChannelCloseConfirm, Serialization.Core.Channel.MessageChannelCloseConfirm>();
            messageRegistry.RegisterMessage<MessageChannelCloseInit, Serialization.Core.Channel.MessageChannelCloseInit>();
            messageRegistry.RegisterMessage<MessageChannelOpenAck, Serialization.Core.Channel.MessageChannelOpenAck>();
            messageRegistry.RegisterMessage<MessageChannelOpenConfirm, Serialization.Core.Channel.MessageChannelOpenConfirm>();
            messageRegistry.RegisterMessage<MessageChannelOpenInit, Serialization.Core.Channel.MessageChannelOpenInit>();
            messageRegistry.RegisterMessage<MessageChannelOpenTry, Serialization.Core.Channel.MessageChannelOpenTry>();
            messageRegistry.RegisterMessage<MessageReceivePacket, Serialization.Core.Channel.MessageReceivePacket>();
            messageRegistry.RegisterMessage<MessageTimeout, Serialization.Core.Channel.MessageTimeout>();
            messageRegistry.RegisterMessage<MessageTimeoutOnClose, Serialization.Core.Channel.MessageTimeoutOnClose>();

            messageRegistry.RegisterMessage<MessageCreateClient, Serialization.Core.Client.MessageCreateClient>();
            messageRegistry.RegisterMessage<MessageSubmitMisbehaviour, Serialization.Core.Client.MessageSubmitMisbehaviour>();
            messageRegistry.RegisterMessage<MessageUpdateClient, Serialization.Core.Client.MessageUpdateClient>();

            messageRegistry.RegisterMessage<MessageConnectionOpenAck, Serialization.Core.Connection.MessageConnectionOpenAck>();
            messageRegistry.RegisterMessage<MessageConnectionOpenConfirm, Serialization.Core.Connection.MessageConnectionOpenConfirm>();
            messageRegistry.RegisterMessage<MessageConnectionOpenInit, Serialization.Core.Connection.MessageConnectionOpenInit>();
            messageRegistry.RegisterMessage<MessageConnectionOpenTry, Serialization.Core.Connection.MessageConnectionOpenTry>();
        }
    }
}
