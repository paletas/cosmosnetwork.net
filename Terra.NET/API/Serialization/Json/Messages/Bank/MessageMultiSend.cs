using Cosmos.SDK.Protos.Bank;
using Google.Protobuf;
using System.Text.Json;

namespace Terra.NET.API.Serialization.Json.Messages.Bank
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageMultiSend(MessageMultiSendInputOutput[] Inputs, MessageMultiSendInputOutput[] Outputs)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "bank/MsgMultiSend";
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgMultiSend";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var send = new MsgMultiSend();
            send.Inputs.AddRange(this.Inputs.Select(i => i.ToInputProto()));
            send.Outputs.AddRange(this.Outputs.Select(i => i.ToOutputProto()));
            return send;
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Bank.MessageMultiSend(this.Inputs.Select(i => i.ToModel()).ToArray(), this.Outputs.Select(o => o.ToModel()).ToArray());
        }
    }

    internal record MessageMultiSendInputOutput(string Address, DenomAmount[] Coins)
    {
        internal Input ToInputProto()
        {
            var input = new Input
            {
                Address = this.Address
            };
            input.Coins.AddRange(this.Coins.Select(c => c.ToProto()).ToArray());
            return input;
        }

        internal Output ToOutputProto()
        {
            var output = new Output
            {
                Address = this.Address
            };
            output.Coins.AddRange(this.Coins.Select(c => c.ToProto()).ToArray());
            return output;
        }

        internal NET.Messages.Bank.MessageMultiSendInputOutput ToModel()
        {
            return new NET.Messages.Bank.MessageMultiSendInputOutput(this.Address, this.Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}
