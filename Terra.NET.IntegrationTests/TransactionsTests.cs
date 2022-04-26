using FluentAssertions;
using FluentAssertions.Execution;
using System.Threading.Tasks;
using Xunit;

namespace Terra.NET.IntegrationTests
{
    public class TransactionsTests : IClassFixture<TestTerraApi>
    {
        private readonly TerraApi _api;

        public TransactionsTests(TestTerraApi api)
        {
            this._api = api;
        }

        [Fact]
        public async Task GetTransactionByHash()
        {
            const string TransactionHash = "19308B3FAC4D30EEF0ADCD5E19D25F1DC3815AECA0EEE5C8C1B85441587E2D24";

            using var _ = new AssertionScope();

            var tx = await this._api.Transactions.GetTransaction(TransactionHash);

            tx.Should().NotBeNull();

            if (tx is not null)
            {
                tx.Hash.Should().Be(TransactionHash);
            }
        }

        [Fact]
        public async Task GetTransactionByBlock()
        {
            const ulong BlockHeight = 8535975;

            using var _ = new AssertionScope();

            var txs = this._api.Transactions.GetTransactions(BlockHeight);

            await foreach (var tx in txs)
            {
                tx.Should().NotBeNull();

                if (tx is not null)
                {
                    tx.Height.Should().Be(BlockHeight);
                }
            }
        }

        [Fact]
        public async Task GetTransactionByBlock_ShouldBeEquivalent_ToTransationHash()
        {
            const ulong BlockHeight = 8535975;

            using var _ = new AssertionScope();

            var txs = this._api.Transactions.GetTransactions(BlockHeight);

            await foreach (var tx in txs)
            {
                tx.Should().NotBeNull();
                var txHash = await this._api.Transactions.GetTransaction(tx.Hash);

                if (txHash is not null)
                {
                    tx.Should().BeEquivalentTo(txHash);
                }
            }
        }
    }
}