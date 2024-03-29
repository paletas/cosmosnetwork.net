﻿using CosmosNetwork.Serialization;
using ProtoBuf;
using System.Text.Json.Serialization;

namespace CosmosNetwork.Modules.Gov.Serialization
{
    [ProtoContract]
    internal record MessageDeposit(
        [property: ProtoMember(1, Name = "proposal_id")] ulong ProposalId,
        [property: ProtoMember(2, Name = "depositor"), JsonPropertyName("depositor")] string DepositorAddress,
        [property: ProtoMember(3, Name = "amount")] DenomAmount[] Amount) : SerializerMessage(Gov.MessageDeposit.COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgDeposit";

        protected internal override Message ToModel()
        {
            return new Gov.MessageDeposit(
                this.ProposalId,
                this.DepositorAddress,
                this.Amount.Select(c => c.ToModel()).ToArray());
        }
    }
}

