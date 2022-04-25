using Cosmos.SDK.Protos.Gov;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Gov
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageSubmitProposal(IProposal Content, [property: JsonPropertyName("proposer")] string ProposerAddress, DenomAmount[] InitialDeposit)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsMsgSubmitProposalSwap";
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgSubmitProposal";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Gov.MessageSubmitProposal(
                this.Content.ToModel(),
                this.ProposerAddress,
                this.InitialDeposit.Select(c => c.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var submitProposal = new MsgSubmitProposal
            {
                Content = this.Content.PackAny(),
                Proposer = this.ProposerAddress
            };
            submitProposal.InitialDeposit.AddRange(this.InitialDeposit.Select(c => c.ToProto()));
            return submitProposal;
        }
    }

    internal interface IProposal
    {
        Any PackAny();

        NET.Messages.Gov.Proposal ToModel();
    }

    internal record TextProposal(string Title, string Description) : IProposal
    {
        public Any PackAny()
        {
            var textProposal = new Cosmos.SDK.Protos.Gov.TextProposal()
            {
                Title = this.Title,
                Description = this.Description
            };

            return new Any
            {
                TypeUrl = Cosmos.SDK.Protos.Gov.TextProposal.Descriptor.Name,
                Value = textProposal.ToByteString()
            };
        }

        public NET.Messages.Gov.Proposal ToModel()
        {
            return new NET.Messages.Gov.TextProposal(this.Title, this.Description);
        }
    }
}
