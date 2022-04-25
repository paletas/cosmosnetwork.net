using Cosmos.SDK.Protos.Gov;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Gov
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageDeposit(ulong ProposalId, [property: JsonPropertyName("depositor")] string DepositorAddress, DenomAmount[] Amount)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "gov/MsgDeposit";
        public const string COSMOS_DESCRIPTOR = "/cosmos.gov.v1beta1.MsgDeposit";

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Gov.MessageDeposit(this.ProposalId, this.DepositorAddress, this.Amount.Select(c => c.ToModel()).ToArray());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var deposit = new MsgDeposit
            {
                ProposalId = this.ProposalId,
                Depositor = this.DepositorAddress
            };
            deposit.Amount.AddRange(this.Amount.Select(amt => amt.ToProto()));
            return deposit;
        }
    }
}

