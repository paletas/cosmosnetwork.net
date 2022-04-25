using Cosmos.SDK.Protos.Bank;
using Google.Protobuf;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Terra.NET.API.Serialization.Json.Messages.Bank
{
    [MessageDescriptor(TerraType = TERRA_DESCRIPTOR, CosmosType = COSMOS_DESCRIPTOR)]
    internal record MessageSend(string FromAddress, string ToAddress, [property: JsonPropertyName("amount")] DenomAmount[] Coins)
        : Message(TERRA_DESCRIPTOR, COSMOS_DESCRIPTOR)
    {
        public const string TERRA_DESCRIPTOR = "bank/MsgSend";
        public const string COSMOS_DESCRIPTOR = "/cosmos.bank.v1beta1.MsgSend";

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var send = new MsgSend
            {
                FromAddress = FromAddress,
                ToAddress = ToAddress
            };
            send.Amount.AddRange(this.Coins.Select(c => c.ToProto()));
            return send;
        }

        internal override NET.Message ToModel()
        {
            return new NET.Messages.Bank.MessageSend(this.FromAddress, this.ToAddress, this.Coins.Select(c => c.ToModel()).ToArray());
        }
    }
}
