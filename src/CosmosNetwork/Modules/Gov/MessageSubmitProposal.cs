using CosmosNetwork.Modules.Gov.Proposals;

namespace CosmosNetwork.Modules.Gov
{
    public record MessageSubmitProposal(string MessageType, IProposal Proposal, CosmosAddress Proposer, Coin[] InitialDeposit) : Message(MessageType)
    {
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1.MsgSubmitProposal";
        public const string COSMOS_BETA_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgSubmitProposal";

        public override CosmosNetwork.Serialization.SerializerMessage ToSerialization()
        {
            return new Serialization.MessageSubmitProposal(
                this.Proposer.Address,
                this.InitialDeposit.Select(coin => coin.ToSerialization()).ToArray())
            {
                Content = this.Proposal.ToSerialization()
            };
        }
    }
}