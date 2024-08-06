using CosmosNetwork.Ibc.Applications.Fees;
using CosmosNetwork.Ibc.Applications.Transfer;
using CosmosNetwork.Ibc.Core.Channel;
using CosmosNetwork.Ibc.Core.Client;
using CosmosNetwork.Ibc.Core.Connection;
using CosmosNetwork.Ibc.Serialization.Core.Client.Proposals;
using CosmosNetwork.Modules;
using CosmosNetwork.Modules.Gov;
using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Ibc
{
    internal class IbcModule(GovModule govModule) : ICosmosMessageModule
    {
        public IbcModule([ServiceKey] string serviceKey, IServiceProvider serviceProvider)
            : this(serviceProvider.GetRequiredKeyedService<GovModule>(serviceKey))
        { }
        
        private readonly ProposalsRegistry _proposalsRegistry = govModule.ProposalsRegistry;

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            this._proposalsRegistry.Register<ClientUpdateProposal>(ClientUpdateProposal.ProposalType);

            messageRegistry.RegisterMessage<MessagePayPacketFee, Serialization.Applications.Fees.MessagePayPacketFee>(MessagePayPacketFee.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessagePayPacketFeeAsync, Serialization.Applications.Fees.MessagePayPacketFeeAsync>(MessagePayPacketFeeAsync.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageRegisterCounterpartyAddress, Serialization.Applications.Fees.MessageRegisterCounterpartyAddress>(MessageRegisterCounterpartyAddress.COSMOS_DESCRIPTOR);

            messageRegistry.RegisterMessage<MessageTransfer, Serialization.Applications.Transfer.MessageTransfer>(MessageTransfer.COSMOS_DESCRIPTOR);

            messageRegistry.RegisterMessage<MessageAcknowledgement, Serialization.Core.Channel.MessageAcknowledgement>(MessageAcknowledgement.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelCloseConfirm, Serialization.Core.Channel.MessageChannelCloseConfirm>(MessageChannelCloseConfirm.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelCloseInit, Serialization.Core.Channel.MessageChannelCloseInit>(MessageChannelCloseInit.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelOpenAck, Serialization.Core.Channel.MessageChannelOpenAck>(MessageChannelOpenAck.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelOpenConfirm, Serialization.Core.Channel.MessageChannelOpenConfirm>(MessageChannelOpenConfirm.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelOpenInit, Serialization.Core.Channel.MessageChannelOpenInit>(MessageChannelOpenInit.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageChannelOpenTry, Serialization.Core.Channel.MessageChannelOpenTry>(MessageChannelOpenTry.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageReceivePacket, Serialization.Core.Channel.MessageReceivePacket>(MessageReceivePacket.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageTimeout, Serialization.Core.Channel.MessageTimeout>(MessageTimeout.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageTimeoutOnClose, Serialization.Core.Channel.MessageTimeoutOnClose>(MessageTimeoutOnClose.COSMOS_DESCRIPTOR);

            messageRegistry.RegisterMessage<MessageCreateClient, Serialization.Core.Client.MessageCreateClient>(MessageCreateClient.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageSubmitMisbehaviour, Serialization.Core.Client.MessageSubmitMisbehaviour>(MessageSubmitMisbehaviour.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageUpdateClient, Serialization.Core.Client.MessageUpdateClient>(MessageUpdateClient.COSMOS_DESCRIPTOR);

            messageRegistry.RegisterMessage<MessageConnectionOpenAck, Serialization.Core.Connection.MessageConnectionOpenAck>(MessageConnectionOpenAck.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageConnectionOpenConfirm, Serialization.Core.Connection.MessageConnectionOpenConfirm>(MessageConnectionOpenConfirm.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageConnectionOpenInit, Serialization.Core.Connection.MessageConnectionOpenInit>(MessageConnectionOpenInit.COSMOS_DESCRIPTOR);
            messageRegistry.RegisterMessage<MessageConnectionOpenTry, Serialization.Core.Connection.MessageConnectionOpenTry>(MessageConnectionOpenTry.COSMOS_DESCRIPTOR);
        }
    }
}
