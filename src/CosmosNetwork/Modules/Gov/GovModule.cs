﻿using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Modules.Gov.Serialization.Proposals.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosNetwork.Modules.Gov
{
    public class GovModule : ICosmosMessageModule
    {
        private readonly IServiceCollection _services;

        public GovModule(IServiceCollection services)
        {
            this._services = services;

            this.ProposalsRegistry = new ProposalsRegistry();
        }

        public ProposalsRegistry ProposalsRegistry { get; init; }

        public void ConfigureModule(CosmosApiOptions cosmosOptions, CosmosMessageRegistry messageRegistry)
        {
            messageRegistry.RegisterMessage<MessageDeposit, Serialization.MessageDeposit>();
            messageRegistry.RegisterMessage<MessageSubmitProposal, Serialization.MessageSubmitProposal>();
            messageRegistry.RegisterMessage<MessageVote, Serialization.MessageVote>();
            messageRegistry.RegisterMessage<MessageVoteWeighted, Serialization.MessageVoteWeighted>();

            _services.AddSingleton(this.ProposalsRegistry);
            cosmosOptions.JsonSerializerOptions.Converters.Add(new ProposalConverter(this.ProposalsRegistry));

            this.ProposalsRegistry.Register<TextProposal>(TextProposal.ProposalType);
        }
    }
}
