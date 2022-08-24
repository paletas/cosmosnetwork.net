﻿using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Gov
{
    [CosmosMessage(COSMOS_DESCRIPTOR)]
    public record MessageSubmitProposal(IProposal Proposal, CosmosAddress Proposer, Coin[] InitialDeposit) : Message
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgSubmitProposal";

        protected internal override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSubmitProposal(
                Proposer.Address,
                InitialDeposit.Select(coin => new CosmosNetwork.Serialization.DenomAmount(coin.Denom, coin.Amount)).ToArray())
            {
                Content = Proposal.ToSerialization()
            };
        }
    }
}