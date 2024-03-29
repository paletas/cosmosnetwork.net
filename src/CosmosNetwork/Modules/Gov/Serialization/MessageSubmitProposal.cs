﻿using CosmosNetwork.Modules.Gov.Serialization.Proposals;
using CosmosNetwork.Serialization;
using CosmosNetwork.Serialization.Proto;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [ProtoContract]
    internal record MessageSubmitProposal(
        [property: ProtoMember(3, Name = "proposer"), JsonPropertyName("proposer")] string ProposerAddress,
        [property: ProtoMember(2, Name = "initial_deposit")] DenomAmount[] InitialDeposit) : SerializerMessage(Gov.MessageSubmitProposal.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsMsgSubmitProposalSwap";

        [ProtoIgnore]
        public IProposal Content { get; set; } = null!;

        [JsonIgnore]
        public Any ContentPack
        {
            get => Any.Pack((IProposalImplementation)this.Content);
            set => this.Content = value.Unpack<IProposalImplementation>() ?? throw new InvalidOperationException();
        }

        protected internal override Message ToModel()
        {
            return new Gov.MessageSubmitProposal(
                this.Content.ToModel(),
                this.ProposerAddress,
                this.InitialDeposit.Select(c => c.ToModel()).ToArray());
        }
    }
}
