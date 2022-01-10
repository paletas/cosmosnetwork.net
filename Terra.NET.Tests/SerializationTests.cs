using FluentAssertions;
using FluentAssertions.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terra.NET.API.Serialization;
using Xunit;

namespace Terra.NET.Tests
{
    public class SerializationTests
    {
        private JsonSerializerOptions _serializationOptions;

        public SerializationTests()
        {
            _serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
        }

        [Fact]
        public void CanDeserializeBlockTransactions()
        {
            var transactionBlock = JsonSerializer.Deserialize<BlockTransaction>(JsonExamples.TransactionBlock, _serializationOptions);

            transactionBlock.Should().NotBeNull();
            if (transactionBlock == null) return;

            transactionBlock.Id.Should().Be(171051354);
            transactionBlock.ChainId.Should().Be("columbus-5");
            transactionBlock.Hash.Should().Be("E672E73C2C23350425E9BFF5BB9FC6A644BD229BEE2F2A94449DC44A56669C9C");
            transactionBlock.GasWanted.Should().Be(2157800);
            transactionBlock.GasUsed.Should().Be(1264459);
            transactionBlock.Timestamp.Should().Be(30.October(2021).At(16, 22, 58).AsUtc());
            transactionBlock.Height.Should().Be(5105879);

            transactionBlock.Details.Should().NotBeNull();
            transactionBlock.Details.TransactionType.Should().Be("core/StdTx");
            transactionBlock.Details.Value.Should().NotBeNull();

            transactionBlock.Details.Value.Fees.Should().NotBeNull();
            transactionBlock.Details.Value.Messages.Should().NotBeNull().And.NotBeEmpty();
            transactionBlock.Details.Value.Memo.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public void CanDeserializeExecuteContractMessages()
        {
            var executeContractMessage = JsonSerializer.Deserialize<Message>(JsonExamples.ExecuteContractMessage, _serializationOptions);

            executeContractMessage.Should().NotBeNull();
            if (executeContractMessage == null) return;

            executeContractMessage.MessageType.Should().Be("wasm/MsgExecuteContract");
            executeContractMessage.Value.Should().NotBeNull();

            executeContractMessage.Value.Coins.Should().NotBeNull().And.NotBeEmpty();
            executeContractMessage.Value.Sender.Should().Be("terra12hjzrwvntaxndt2fwl3j0qyw7vqjr96d0j5qpr");
            executeContractMessage.Value.Contract.Should().Be("terra1fxwelge6mf5l6z0rjpylzcfq9w9tw2q7tewaf5");
            executeContractMessage.Value.ExecuteMessage.Should().NotBeNull();
        }

        [Fact]
        public void CanDeserializeTransactionFees()
        {
            var fees = JsonSerializer.Deserialize<Fee>(JsonExamples.TransactionFee, _serializationOptions);

            fees.Should().NotBeNull();
            if (fees == null) return;

            fees.Gas.Should().Be(198435);
            fees.Amount.Should().NotBeNull().And.NotBeEmpty().And.ContainSingle();
            fees.Amount[0].Amount.Should().Be(29766);
            fees.Amount[0].Denom.Should().Be("uusd");
        }
    }
}