using Cosmos.SDK.Protos.Bank;
using Google.Protobuf;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TerraMoney.SDK.Core.Protos.Market;
using TerraMoney.SDK.Core.Protos.WASM;

namespace Terra.NET.API.Serialization.Json
{
    internal abstract record Message(string MessageType)
    {
        internal abstract IMessage ToProto(JsonSerializerOptions? serializerOptions = null);

        internal Google.Protobuf.WellKnownTypes.Any PackAny(JsonSerializerOptions? serializerOptions = null)
        {
            var anyPack = new Google.Protobuf.WellKnownTypes.Any
            {
                TypeUrl = MessageType,
                Value = ToProto(serializerOptions).ToByteString()
            };
            return anyPack;
        }

        internal abstract Terra.NET.Message ToModel();
    };

    internal record MessageSend(string FromAddress, string ToAddress, DenomAmount[] Coins)
        : Message("/cosmos.bank.v1beta1.MsgSend")
    {
        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var send = new MsgSend
            {
                FromAddress = FromAddress,
                ToAddress = ToAddress
            };
            send.Amount.AddRange(Coins.Select(c => c.ToProto()));
            return send;
        }

        internal override NET.Message ToModel()
        {
            return new NET.MessageSend(this.FromAddress, this.ToAddress, this.Coins.Select(c => c.ToModel()).ToArray());
        }
    }

    internal record MessageExecuteContract<T>(DenomAmount[] Coins, string Sender, string Contract, [property: JsonPropertyName("execute_msg")] T ExecuteMessage)
        : Message("/terra.wasm.v1beta1.MsgExecuteContract")
    {
        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            string executeMessageJson = JsonSerializer.Serialize(ExecuteMessage, serializerOptions);

            var execute = new MsgExecuteContract
            {
                Sender = Sender,
                Contract = Contract,
                ExecuteMsg = ByteString.CopyFrom(executeMessageJson, Encoding.UTF8)
            };
            execute.Coins.AddRange(Coins.Select(c => c.ToProto()));
            return execute;
        }

        internal override NET.Message ToModel()
        {
            return new NET.MessageExecuteContract<T>(this.Coins.Select(c => c.ToModel()).ToArray(), this.Sender, this.Contract, this.ExecuteMessage);
        }
    }

    internal record MessageSwap(string Trader, string AskDenom, DenomAmount OfferCoin)
        : Message("/terra.market.v1beta1.MsgSwap")
    {
        internal override NET.Message ToModel()
        {
            return new NET.MessageSwap(this.Trader, this.AskDenom, this.OfferCoin.ToModel());
        }

        internal override IMessage ToProto(JsonSerializerOptions? serializerOptions = null)
        {
            var swap = new MsgSwap
            {
                Trader = this.Trader,
                AskDenom = this.AskDenom,
                OfferCoin = this.OfferCoin.ToProto()
            };

            return swap;
        }
    }
}
